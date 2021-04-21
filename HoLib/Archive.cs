using HoLib.Helpers;
using HoLib.Models;
using HoLib.Sections;
using HoLib.Static;
using System;
using System.Collections.Generic;
using System.IO;

namespace HoLib
{
    public class Archive
    {
        public string ArchivePath { get; }
        public IEnumerable<AssetEntry> Assets => Section.GetAssetEntries();

        public readonly byte[] Data;
        public readonly MAST MAST;
        public readonly TCES TCES;
        public readonly string StringTable;
        public readonly Section Section;        

        public Archive(string filename)
        {
            ArchivePath = filename;

            using var stream = File.OpenRead(ArchivePath);
            using var reader = new EndianAwareBinaryReader(stream);

            reader.AssertTag(BlockType.HEL, BlockType.HEB);
            reader.IsBigEndian = reader.PeekChar() == 0;

            // contains file offsets and a string block containing
            // archive details. probably includes page size and offset
            // to MAST block
            Data = reader.ReadBytes(0x800 - 4);

            reader.AssertTag(BlockType.MAST);
            MAST = new MAST(reader);

            // this is reversed to signify this is the section header
            // for multi-sectioned files (which don't exist)
            reader.AssertTag(BlockType.TCES, BlockType.SECT);
            TCES = new TCES(reader);

            // can't find references to this?
            StringTable = reader.ReadString(MAST.StringTableSize);

            reader.Seek(TCES.PageOffset << 11, SeekOrigin.Begin);

            Section = new Section(reader);
        }

        public void Extract(AssetEntry entry, string outputDirectory)
        {
            using var stream = File.OpenRead(ArchivePath);
            Extract(entry, outputDirectory, stream);
        }

        public void Extract(AssetEntry entry, string outputDirectory, Stream stream)
        {
            if (entry == null)
                return;

            // create folder structure
            outputDirectory = Utils.CreateDirectory(outputDirectory,
                Section.SectionName.TrimStart('/'),
                entry.Flags.ToString());

            // generate filename
            var filename = $"{Path.GetFileNameWithoutExtension(entry.Name)}.{entry.AssetID:X8}";

            if (Enum.IsDefined(entry.AssetType))
                filename += $".{entry.AssetType}";

            filename += Path.GetExtension(entry.Name);

            entry.Extract(stream, Path.Combine(outputDirectory, filename));
        }
    }
}
