using HoLib.Helpers;
using HoLib.Models;
using HoLib.Static;
using System;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// A single asset's meta data
    /// </summary>
    public class AssetEntry : ISection
    {
        public string Name { get; internal set; }
        public Flags Flags => Layer.Flags;

        public int Size; // aligned size
        public int RelativeDataOffset;
        public int DataSize;
        public readonly int Unknown1; // multiple of 4, seen 4, 16, 32
        public readonly ulong AssetID;
        public readonly AssetType AssetType;
        public readonly short Unknown2; // 0 or 1
        public readonly short Unknown3; // always 1

        private readonly Layer Layer;
        private byte[] OverrideFileData;

        public AssetEntry(EndianAwareBinaryReader reader, Layer layer)
        {
            Layer = layer;

            Size = reader.ReadInt32();
            RelativeDataOffset = reader.ReadInt32();
            DataSize = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            AssetID = reader.ReadUInt64(); // this might be a hash of the filepath?
            AssetType = (AssetType)reader.ReadUInt32();
            Unknown2 = reader.ReadInt16();
            Unknown3 = reader.ReadInt16();
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32(Size);
            writer.WriteInt32(RelativeDataOffset);
            writer.WriteInt32(DataSize);
            writer.WriteInt32(Unknown1);
            writer.WriteUInt64(AssetID);
            writer.WriteUInt32((uint)AssetType);
            writer.WriteInt16(Unknown2);
            writer.WriteInt16(Unknown3);
        }

        public byte[] ReadData(Stream stream)
        {
            var buffer = new byte[DataSize];
            stream.Seek((Layer.PageOffset << 11) + RelativeDataOffset, SeekOrigin.Begin);
            stream.Read(buffer);
            return buffer;
        }

        public void WriteData(EndianAwareBinaryWriter writer, Stream source)
        {
            writer.WriteBytes(OverrideFileData ?? ReadData(source));
            writer.Align(0x40);
            OverrideFileData = null;
        }

        public void SetData(byte[] data)
        {
            if((OverrideFileData = data) != null)
            {
                DataSize = data.Length;
                Size = (DataSize + 63) / 64 << 6; // DRU 64
            }
        }

        public override bool Equals(object obj)
        {
            return obj is AssetEntry other &&
                other.Flags == Flags &&
                other.AssetID == AssetID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Flags, AssetID);
        }
    }
}
