using HoLib.Sections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace HoExtractor
{
    class AssetEntryView : ReadOnlyCollection<AssetEntry>, ITypedList, IBindingListView
    {
        public event ListChangedEventHandler ListChanged;
        public bool AllowEdit => false;
        public bool AllowNew => false;
        public bool AllowRemove => false;
        public bool IsSorted => isSorted;
        public ListSortDirection SortDirection => sortDirection;
        public PropertyDescriptor SortProperty => sortProperty;
        public bool SupportsChangeNotification => false;
        public bool SupportsSearching => true;
        public bool SupportsSorting => true;

        public string Filter { get => filter; set => ApplyFilter(value); }
        public ListSortDescriptionCollection SortDescriptions => throw new NotImplementedException();
        public bool SupportsAdvancedSorting => false;
        public bool SupportsFiltering => true;

        private bool isSorted = false;
        private PropertyDescriptor sortProperty = null;
        private ListSortDirection sortDirection = ListSortDirection.Ascending;
        private string filter = null;

        private readonly IReadOnlyDictionary<AssetEntry, string> SourceList;
        private readonly PropertyDescriptorCollection Properties;

        public AssetEntryView(IEnumerable<AssetEntry> source) : base(new List<AssetEntry>())
        {
            // Properties
            var propertyList = new List<PropertyDescriptor>
            {
                new Property<string>("Name", (item, p) => item.Name),
                new Property<uint>("Flags", (item, p) => item.Flags),
                new Property<ulong>("AssetID", (item, p) => item.AssetID),
                new Property<string>("AssetType", (item, p) => Utils.EnumFormatter(item.AssetType)),
                new Property<int>("DataSize", (item, p) => item.DataSize),
            };

            Properties = new PropertyDescriptorCollection(propertyList.ToArray());
            SourceList = source.ToDictionary(x => x, x => Utils.Stringify(x, Properties));            

            ((List<AssetEntry>)Items).AddRange(source);
        }

        #region Unused

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public string GetListName(PropertyDescriptor[] listAccessors) => null;

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            sortProperty = property;
            sortDirection = direction;

            if (sortProperty == null)
                return;
            if (Items is not List<AssetEntry> list)
                return;

            list.Sort(Compare);
            isSorted = true;
        }

        public void ApplyFilter(string filter)
        {
            if (Items is not List<AssetEntry> source)
                return;
            if (Filter == filter)
                return;

            if (string.IsNullOrEmpty(filter))
            {
                source.Clear();
                source.AddRange(SourceList.Keys);                
            }
            else
            {
                source.Clear();
                filter = filter.ToUpperInvariant();

                foreach (var item in SourceList)
                    if (item.Value.Contains(filter))
                        Items.Add(item.Key);

                ApplySort(sortProperty, sortDirection);
            }

            this.filter = filter;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) => Properties;

        public void RemoveSort()
        {
            isSorted = false;
            sortProperty = null;
        }

        public void RemoveFilter()
        {
            Filter = null;
        }

        protected void OnListChanged(ListChangedEventArgs e)
        {
            ListChanged?.Invoke(this, e);
        }

        private int Compare(AssetEntry lhs, AssetEntry rhs)
        {
            var result = OnComparison(lhs, rhs);

            if (sortDirection == ListSortDirection.Descending)
                result = -result;

            return result;
        }

        private int OnComparison(AssetEntry lhs, AssetEntry rhs)
        {
            var lhsValue = lhs == null ? null : sortProperty.GetValue(lhs);
            var rhsValue = rhs == null ? null : sortProperty.GetValue(rhs);

            if (lhsValue == null)
                return rhsValue == null ? 0 : -1; // nulls are equal

            if (rhsValue == null)
                return 1; // first has value, second doesn't

            if (lhsValue is string || lhsValue is Enum)
                return string.Compare(lhsValue.ToString(), rhsValue.ToString(), true);

            if (lhsValue is IComparable comparable)
                return comparable.CompareTo(rhsValue);

            if (lhsValue.Equals(rhsValue))
                return 0; // both are the same

            // default to string compare
            return string.Compare(lhsValue.ToString(), rhsValue.ToString(), true);
        }     

        private class Property<T> : PropertyDescriptor
        {
            private readonly Func<AssetEntry, Property<T>, T> GetValueImpl;
            public Property(string name, Func<AssetEntry, Property<T>, T> getValue) : base(name, null) => GetValueImpl = getValue;
            public override Type ComponentType => typeof(AssetEntry);
            public override Type PropertyType => typeof(T);
            public override object GetValue(object component) => GetValueImpl((AssetEntry)component, this);
            public override bool IsReadOnly => true;
            public override bool CanResetValue(object component) => false;
            public override void ResetValue(object component) => throw new NotSupportedException();
            public override void SetValue(object component, object value) => throw new NotSupportedException();
            public override bool ShouldSerializeValue(object component) => false;
        }
    }
}
