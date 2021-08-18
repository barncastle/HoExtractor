using HoLib.Helpers;
using HoLib.Models;
using System.Collections.Generic;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// A layer containing groups of DirectoryEntrys
    /// </summary>
    public class DirectoryLayer : ILayer<DirectoryEntry>
    {
        public readonly int Size;
        public readonly int AmountOfEntries;
        public readonly int DataSize;
        public readonly int Unknown2;
        public readonly int Unknown3;
        public readonly int Unknown4;
        public readonly int Unknown5;
        public readonly Dictionary<ulong, DirectoryEntry> Entries;
        public readonly Layer Layer;

        public DirectoryLayer(EndianAwareBinaryReader reader, Layer layer)
        {
            Layer = layer;
            Entries = new Dictionary<ulong, DirectoryEntry>();

            Size = reader.ReadInt32();
            AmountOfEntries = reader.ReadInt32();
            DataSize = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();

            ReadEntries(reader);
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteString("PSLD", 4);
            writer.WriteInt32(Size);
            writer.WriteInt32(Entries.Count);
            writer.WriteInt32(DataSize);
            writer.WriteInt32(Unknown2);
            writer.WriteInt32(Unknown3);
            writer.WriteInt32(Unknown4);
            writer.WriteInt32(Unknown5);
        }

        public bool ContainsEntry(ulong uid)
        {
            return Entries.ContainsKey(uid);
        }

        public bool TryGetEntry(ulong uid, out DirectoryEntry entry)
        {
            return Entries.TryGetValue(uid, out entry);
        }

        public void ReadEntries(EndianAwareBinaryReader reader)
        {
            var offset = Layer.PageOffset << 11;
            reader.Seek(offset, SeekOrigin.Begin);

            // sizes for each entry
            var entrySizes = new int[AmountOfEntries];
            for (var i = 0; i < AmountOfEntries; i++)
                entrySizes[i] = reader.ReadInt32();

            // skip padding
            reader.Seek(offset + DataSize, SeekOrigin.Begin);

            // read directories
            Entries.EnsureCapacity(AmountOfEntries);
            for (var i = 0; i < AmountOfEntries; i++)
            {
                var entry = new DirectoryEntry(reader, entrySizes[i]);
                Entries.Add(entry.AssetID, entry);
            }
        }

        public void WriteEntries(EndianAwareBinaryWriter writer, Stream source)
        {
            var offset = writer.BaseStream.Position;

            // sizes for each entry
            foreach (var entry in Entries)
                writer.WriteInt32(entry.Value.Size);

            // padding
            writer.Align(0x40); // Size - (AmountOfEntries * 4)

            // write directories
            foreach (var entry in Entries)
                entry.Value.Write(writer);

            writer.Align(0x800);

            // update layer data
            Layer.PageOffset = (int)(offset >> 11);
        }
    }
}
