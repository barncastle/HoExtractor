using HoLib.Helpers;
using HoLib.Models;
using HoLib.Static;
using System.Collections.Generic;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// A layer containing groups of AssetEntrys
    /// </summary>
    public class AssetLayer : ILayer<AssetEntry>
    {
        public readonly int Size;
        public readonly int SectionCount;
        public readonly int Unknown1;
        public readonly AssetSection[] Sections;
        public readonly List<AssetEntry> Entries;
        public readonly Layer Layer;

        public AssetLayer(EndianAwareBinaryReader reader, Layer layer)
        {
            Layer = layer;

            Entries = new List<AssetEntry>();
            Size = reader.ReadInt32();
            SectionCount = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            Sections = Utils.CreateArray<AssetSection>(SectionCount, reader);

            ReadEntries(reader);
        }

        public bool ContainsEntry(ulong uid)
        {
            return Entries.FindIndex(x => x.AssetID == uid) > -1;
        }

        public bool TryGetEntry(ulong uid, out AssetEntry entry)
        {
            return (entry = Entries.Find(x => x.AssetID == uid)) != null;
        }

        public void ReadEntries(EndianAwareBinaryReader reader)
        {
            var offset = Layer.PageOffset << 11;

            for (var i = 0; i < SectionCount; i++)
            {
                if (Sections[i].Type == AssetSectionType.AssetGroup)
                {
                    reader.Seek(offset + Sections[i].StartOffset, SeekOrigin.Begin);

                    var group = new AssetGroup(reader);
                    for (var j = 0; j < group.Count; j++)
                        Entries.Add(new AssetEntry(reader, Layer));
                }
            }
        }
    }
}
