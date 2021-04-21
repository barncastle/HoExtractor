using HoLib.Helpers;

namespace HoLib.Sections
{
    public interface ILayer<T>
    {
        void ReadEntries(EndianAwareBinaryReader reader);
        bool ContainsEntry(ulong uid);
        bool TryGetEntry(ulong uid, out T entry);
    }
}
