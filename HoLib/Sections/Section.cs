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
        public readonly BlockType Tag;
        public readonly int LayerCount;
        public readonly int Unknown1; // always 0
        public readonly int NameOffset; // layers + name + padding OR name offset
        public readonly int NameSize;
        public readonly int SubLayersOffset; // relative offset
        public readonly int SubLayersSize; // combined ILayer header size
        public readonly int Unknown2; // always 0
        public readonly Layer[] Layers;
        public readonly string Name;

        private long Offset;
        private readonly List<ILayer> SubLayers;

        public Section(EndianAwareBinaryReader reader)
        {
            Offset = reader.BaseStream.Position;
            SubLayers = new List<ILayer>();

            Tag = reader.AssertTag(BlockType.SECT);
            LayerCount = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            NameSize = reader.ReadInt32();
            SubLayersOffset = reader.ReadInt32();
            SubLayersSize = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();

            // section data
            Layers = Utils.CreateArray<Layer>(LayerCount, reader);
            Name = reader.ReadCString();
            reader.Seek(Offset + SubLayersOffset, SeekOrigin.Begin); // padding

            // read sub layers
            for (var i = 0; i < LayerCount; i++)
                ReadLayer(reader, i);

            // append internal names to asset entries
            UpdateAssetEntries();
        }

        public void Write(EndianAwareBinaryWriter writer, Stream source)
        {
            Offset = writer.BaseStream.Position;

            writer.WriteTag(Tag);
            writer.WriteInt32(LayerCount);
            writer.WriteInt32(Unknown1);
            writer.WriteInt32(NameOffset);
            writer.WriteInt32(NameSize);
            writer.WriteInt32(SubLayersOffset);
            writer.WriteInt32(SubLayersSize);
            writer.WriteInt32(Unknown2);
            writer.Seek(64 * LayerCount, SeekOrigin.Current); // skip layers
            writer.WriteCString(Name);
            writer.Align(0x20); // set ptr to Offset + StartOfSubLayers
            writer.Seek(SubLayersSize, SeekOrigin.Current); // skip sublayers

            writer.Align(0x800);

            // write sublayer data
            // this also recalcs Layer and SubLayer fields
            for (var i = 0; i < LayerCount; i++)
                SubLayers[i].WriteEntries(writer, source);

            // write updated Layer data
            writer.Seek(Offset + 0x20, SeekOrigin.Begin);
            Array.ForEach(Layers, l => l.Write(writer));

            // write updated SubLayer data
            writer.Seek(Offset + SubLayersOffset, SeekOrigin.Begin);
            SubLayers.ForEach(l => l.Write(writer));

            writer.Seek(0, SeekOrigin.End);
        }

        public IEnumerable<AssetEntry> GetAssetEntries()
        {
            return GetLayers<AssetLayer>()
                .SelectMany(l => l.Groups
                .SelectMany(g => g.Entries));
        }

        private void ReadLayer(EndianAwareBinaryReader reader, int index)
        {
            reader.Seek(Offset + Layers[index].SubLayerOffset, SeekOrigin.Begin);

            var magic = reader.ReadString(4);

            SubLayers.Add(magic switch
            {
                "PSL\0" => new AssetLayer(reader, Layers[index]),
                "PSLD" => new DirectoryLayer(reader, Layers[index]) as ILayer,
                _ => throw new NotImplementedException($"SubLayer {magic}")
            });
        }

        private void UpdateAssetEntries()
        {
            // multiple directory layers can have the same flags
            // but will never duplicate asset ids
            var directories = GetLayers<DirectoryLayer>().ToLookup(x => x.Layer.Flags);

            foreach (var asset in GetLayers<AssetLayer>())
            {
                // layer flags are used to control file overrides so that 
                // assets can be linked by id and the correct variant will load
                // therefore we need to match flags for names
                var directory = directories[asset.Layer.Flags];

                asset.Groups.ForEach(g =>
                {
                    g.Entries.ForEach(e =>
                    {
                        foreach (var d in directory)
                        {
                            if (d.ContainsEntry(e.AssetID))
                                e.Name = d.Entries[e.AssetID].FileName;
                        }
                    });
                });
            }
        }

        private IEnumerable<T> GetLayers<T>() where T : ILayer
        {
            foreach (var layer in SubLayers)
                if (layer is T type)
                    yield return type;
        }
    }
}
