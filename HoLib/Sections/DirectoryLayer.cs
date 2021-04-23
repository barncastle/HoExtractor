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
        public readonly int Unknown1;
        public readonly int AmountOfEntries;
        public readonly int Size;
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

            Unknown1 = reader.ReadInt32();
            AmountOfEntries = reader.ReadInt32();
            Size = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();

            ReadEntries(reader);
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
            reader.Seek(offset + Size, SeekOrigin.Begin);

            // read directories
            Entries.EnsureCapacity(AmountOfEntries);
            for (var i = 0; i < AmountOfEntries; i++)
            {
                var entry = new DirectoryEntry(reader, entrySizes[i]);
                Entries.Add(entry.AssetID, entry);
            }
        }
    }
}
