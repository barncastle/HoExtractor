using HoLib.Helpers;
using HoLib.Models;
using HoLib.Static;
using System;
using System.Collections.Generic;
using System.IO;

namespace HoLib.Sections
{
    /// <summary>
    /// A layer containing groups of AssetEntrys
    /// </summary>
    public class AssetLayer : ILayer<AssetEntry>
    {
        public readonly int Size;
        public readonly int SectionCount;
        public readonly int Unknown1; // always 0
        public readonly AssetSection[] Sections; // (padding, data)[x], padding, group[x]
        public readonly List<AssetGroup> Groups;
        public readonly Layer Layer;

        public AssetLayer(EndianAwareBinaryReader reader, Layer layer)
        {
            Layer = layer;

            Groups = new List<AssetGroup>();
            Size = reader.ReadInt32();
            SectionCount = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            Sections = Utils.CreateArray<AssetSection>(SectionCount, reader);

            ReadEntries(reader);
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteString("PSL\0", 4);
            writer.WriteInt32(Size);
            writer.WriteInt32(SectionCount);
            writer.WriteInt32(Unknown1);
            Array.ForEach(Sections, s => s.Write(writer));
        }

        public bool ContainsEntry(ulong uid)
        {
            return Groups.FindIndex(g => g.ContainsEntry(uid)) > -1;
        }

        public bool TryGetEntry(ulong uid, out AssetEntry entry)
        {
            foreach (var group in Groups)
                if (group.TryGetEntry(uid, out entry))
                    return true;

            entry = null;
            return false;
        }

        public void ReadEntries(EndianAwareBinaryReader reader)
        {
            var offset = Layer.PageOffset << 11;

            for (var i = 0; i < SectionCount; i++)
            {
                if (Sections[i].Type == AssetSectionType.AssetGroup)
                {
                    reader.Seek(offset + Sections[i].StartOffset, SeekOrigin.Begin);
                    Groups.Add(new AssetGroup(reader, Layer));
                }
            }
        }

        public void WriteEntries(EndianAwareBinaryWriter writer, Stream source)
        {
            var groupIndex = 0;
            var offset = writer.BaseStream.Position;

            // align to first section and increment layer offset
            if (Sections[0].Alignment > 0)
                offset += writer.Align(Sections[0].Alignment);

            for (var i = 0; i < Sections.Length; i++)
            {
                // get start offset for section
                var start = writer.GetOffset(offset);

                switch (Sections[i].Type)
                {
                    case AssetSectionType.AssetData:
                        Groups[groupIndex++].WriteData(writer, source, start);
                        break;
                    case AssetSectionType.AssetGroup:
                        Groups[groupIndex++].Write(writer);
                        break;
                    case AssetSectionType.Padding when groupIndex == Groups.Count: // special case
                        writer.Align(0x800);
                        groupIndex = 0; // reset group counter
                        break;
                    case AssetSectionType.Padding when Sections[i].Alignment > 0:
                        writer.Align(Sections[i].Alignment);
                        break;
                }

                Sections[i].StartOffset = start;
                Sections[i].Size = writer.GetOffset(offset) - start;
            }

            // update layer data
            Layer.PageOffset = (int)(offset >> 11);
            Layer.TotalLayerSize = Sections[^1].Size + Sections[^1].StartOffset;
            Layer.TotalLayerSize2 = Layer.TotalLayerSize;

            writer.Align(0x800);
        } 
    }
}
