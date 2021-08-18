using HoLib.Helpers;
using System.IO;

namespace HoLib.Sections
{
    public interface ILayer<T> : ILayer
    {
        void ReadEntries(EndianAwareBinaryReader reader);
        bool ContainsEntry(ulong uid);
        bool TryGetEntry(ulong uid, out T entry);
    }

    public interface ILayer
    {
        public void Write(EndianAwareBinaryWriter writer);
        public void WriteEntries(EndianAwareBinaryWriter writer, Stream source);
    }
}
