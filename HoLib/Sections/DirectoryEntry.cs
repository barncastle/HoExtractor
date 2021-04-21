using HoLib.Helpers;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// An entry containing an asset's internal name and id
    /// </summary>
    public class DirectoryEntry
    {
        public readonly ulong AssetID;
        public readonly int Unknown2;
        public readonly int Unknown3;
        public readonly int Unknown4;
        public readonly int Unknown5;
        public readonly int Unknown6;
        public readonly int Unknown7;
        public readonly string FileName;

        public DirectoryEntry(EndianAwareBinaryReader reader, int size)
        {
            var offset = reader.BaseStream.Position + size;

            AssetID = reader.ReadUInt64();
            Unknown2 = reader.ReadInt32(); // size of next block?
            Unknown3 = reader.ReadInt32(); // file hash of size?
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            Unknown7 = reader.ReadInt32();
            FileName = reader.ReadCString();

            reader.Seek(offset, SeekOrigin.Begin);
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
