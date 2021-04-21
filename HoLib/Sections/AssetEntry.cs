using HoLib.Helpers;
using HoLib.Models;
using HoLib.Static;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// A single asset's meta data
    /// </summary>
    public class AssetEntry
    {
        public string Name { get; internal set; }
        public Flags Flags => Layer.Flags;

        public readonly int Size;
        public readonly int RelativeDataOffset;
        public readonly int DataSize;
        public readonly int Unknown1;
        public readonly ulong AssetID;
        public readonly AssetType AssetType;
        public readonly int Unknown2;
        private readonly Layer Layer;

        public AssetEntry(EndianAwareBinaryReader reader, Layer layer)
        {
            Layer = layer;

            Size = reader.ReadInt32();
            RelativeDataOffset = reader.ReadInt32();
            DataSize = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            AssetID = reader.ReadUInt64(); // this might be a hash of the filepath?
            AssetType = (AssetType)reader.ReadUInt32();
            Unknown2 = reader.ReadInt32();
        }

        public void Extract(Stream stream, string filename)
        {
            File.WriteAllBytes(filename, Read(stream));
        }

        public byte[] Read(Stream stream)
        {
            var buffer = new byte[DataSize];
            stream.Seek((Layer.PageOffset << 11) + RelativeDataOffset, SeekOrigin.Begin);
            stream.Read(buffer);
            return buffer;
        }
    }
}
