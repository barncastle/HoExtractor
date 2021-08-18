using HoLib.Helpers;
using HoLib.Sections;
using System.Collections.Generic;
using System.IO;

namespace HoLib.Models
{
    public class AssetGroup : IModel
    {
        public readonly int Count;
        public readonly int Unknown1; // always -1
        public readonly List<AssetEntry> Entries;

        public AssetGroup(EndianAwareBinaryReader reader, Layer layer)
        {
            Count = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            reader.Seek(0x18, SeekOrigin.Current); // padding, 0x74 filled

            Entries = new List<AssetEntry>(Count);
            for (var i = 0; i < Count; i++)
                Entries.Add(new AssetEntry(reader, layer));
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32(Entries.Count);
            writer.WriteInt32(Unknown1);
            writer.WriteBytes(0x74, 0x18);

            for (var i = 0; i < Entries.Count; i++)
                Entries[i].Write(writer);

            writer.Align(0x40);
        }

        public void WriteData(EndianAwareBinaryWriter writer, Stream source, int offset)
        {
            for(var i = 0; i < Entries.Count; i++)
            {
                Entries[i].WriteData(writer, source);
                Entries[i].RelativeDataOffset = offset;
                offset += Entries[i].Size;
            }
        }

        public bool ContainsEntry(ulong uid)
        {
            return Entries.FindIndex(x => x.AssetID == uid) > -1;
        }

        public bool TryGetEntry(ulong uid, out AssetEntry entry)
        {
            return (entry = Entries.Find(x => x.AssetID == uid)) != null;
        }
    }
}
