using HoLib.Helpers;
using HoLib.Models;
using HoLib.Static;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HoLib.Sections
{
    public class Section
    {
        public readonly int LayerCount;
        public readonly int Unknown1;
        public readonly int SizeOfLayerData;
        public readonly int SizeOfName;
        public readonly int SizeOfChunk;
        public readonly int SizeOfSubLayerData;
        public readonly int Unknown2;
        public readonly Layer[] Layers;
        public readonly string SectionName;

        private readonly long Offset;
        private readonly List<AssetLayer> AssetLayers;
        private readonly List<DirectoryLayer> DirectoryLayers;

        public Section(EndianAwareBinaryReader reader)
        {
            Offset = reader.BaseStream.Position;
            AssetLayers = new List<AssetLayer>();
            DirectoryLayers = new List<DirectoryLayer>();

            reader.AssertTag(BlockType.SECT);
            LayerCount = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            SizeOfLayerData = reader.ReadInt32();
            SizeOfName = reader.ReadInt32();
            SizeOfChunk = reader.ReadInt32();
            SizeOfSubLayerData = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            Layers = Utils.CreateArray<Layer>(LayerCount, reader);
            SectionName = reader.ReadCString();

            // skip padding
            reader.Seek(Offset + SizeOfChunk, SeekOrigin.Begin);

            // read sub layers
            for (var i = 0; i < LayerCount; i++)
                ReadLayer(reader, Layers[i]);

            // append internal names to asset entries
            UpdateAssetEntries();
        }

        public IEnumerable<AssetEntry> GetAssetEntries()
        {
            foreach (var layer in AssetLayers)
                foreach (var entry in layer.Entries)
                    yield return entry;
        }

        private void ReadLayer(EndianAwareBinaryReader reader, Layer layer)
        {
            reader.Seek(Offset + layer.SubLayerOffset, SeekOrigin.Begin);

            var magic = reader.ReadString(4);
            switch (magic)
            {
                case "PSL\0": // Asset layer
                    AssetLayers.Add(new AssetLayer(reader, layer));
                    break;
                case "PSLD": // Directory layer
                    DirectoryLayers.Add(new DirectoryLayer(reader, layer));
                    break;
                default:
                    throw new NotImplementedException($"SubLayer {magic}");
            }
        }

        private void UpdateAssetEntries()
        {
            // multiple directory layers can have the same flags
            // but will never duplicate asset ids
            var directories = DirectoryLayers.ToLookup(x => x.Layer.Flags);

            foreach (var assets in AssetLayers)
            {
                // layer flags are used to control file overrides so that 
                // assets can be linked by id and the correct variant will load
                // therefore we need to match flags for names
                var directory = directories[assets.Layer.Flags];

                foreach (var entry in assets.Entries)
                {
                    foreach (var d in directory)
                        if (d.ContainsEntry(entry.AssetID))
                            entry.Name = d.Entries[entry.AssetID].FileName;
                }
            }
        }
    }
}
