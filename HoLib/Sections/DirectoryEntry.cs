using HoLib.Helpers;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// An entry containing an asset's internal name and id
    /// </summary>
    public class DirectoryEntry : ISection
    {
        public readonly ulong AssetID;
        public readonly int FileNameOffset; // size?
        public readonly int Unknown3;
        public readonly int Unknown4; // always -1
        public readonly int Unknown5; // always 0
        public readonly int Unknown6; // always 0
        public readonly int Unknown7; // always 0
        public readonly string FileName;

        public int Size => (96 + FileName.Length) / 64 << 6; // DRU * pagesize

        public DirectoryEntry(EndianAwareBinaryReader reader, int size)
        {
            var offset = reader.BaseStream.Position + size;

            AssetID = reader.ReadUInt64();
            FileNameOffset = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            Unknown7 = reader.ReadInt32();
            FileName = reader.ReadCString();

            reader.Seek(offset, SeekOrigin.Begin);
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteUInt64(AssetID);
            writer.WriteInt32(FileNameOffset);
            writer.WriteInt32(Unknown3);
            writer.WriteInt32(Unknown4);
            writer.WriteInt32(Unknown5);
            writer.WriteInt32(Unknown6);
            writer.WriteInt32(Unknown7);
            writer.WriteCString(FileName);

            writer.Align(0x40);
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
