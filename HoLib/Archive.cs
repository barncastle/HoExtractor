using HoLib.Helpers;
using HoLib.Models;
using HoLib.Sections;
using HoLib.Static;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace HoLib
{
    public class Archive
    {
        public string ArchivePath { get; }
        public IEnumerable<AssetEntry> Assets => Section.GetAssetEntries();

        public readonly BlockType Tag;
        public readonly byte[] Data;
        public readonly MAST MAST;
        public readonly TCES TCES;
        public readonly string StringTable; // can't find references to this?
        public readonly Section Section;

        public Archive(string filename)
        {
            ArchivePath = filename;

            using var stream = File.OpenRead(ArchivePath);
            using var reader = new EndianAwareBinaryReader(stream);

            Tag = reader.AssertTag(BlockType.HEL, BlockType.HEB);
            reader.IsBigEndian = reader.PeekChar() == 0;

            // contains file offsets and a string block containing
            // archive details. probably includes page size and offset
            // to MAST block
            Data = reader.ReadBytes(0x800 - 4);

            MAST = new MAST(reader);
            TCES = new TCES(reader);
            StringTable = reader.ReadString(MAST.StringTableSize);

            reader.Seek(TCES.PageOffset << 11, SeekOrigin.Begin);

            Section = new Section(reader);
        }

        public void Save(string filename)
        {
            Utils.CreateDirectory(Path.GetDirectoryName(filename));

            using var source = File.OpenRead(ArchivePath);
            using var stream = File.Create(filename);
            using var writer = new EndianAwareBinaryWriter(stream);

            // header
            writer.WriteTag(Tag);
            writer.WriteBytes(Data);
            writer.IsBigEndian = Data[0] == 0;

            MAST.Write(writer);
            TCES.Write(writer);

            writer.WriteString(StringTable, MAST.StringTableSize);
            writer.Align(0x800); // (TCES.PageOffset << 11) - pos

            Section.Write(writer, source);

            // final padding
            writer.Seek(0, SeekOrigin.End);
            writer.Align(0x800);
            stream.Flush(true);
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

            // generate filename
            var filename = string.Concat(
                Path.GetFileNameWithoutExtension(entry.Name), // base name
                $".{entry.AssetID:X8}", // assetId as hex
                Enum.IsDefined(entry.AssetType) ? $".{entry.AssetType}" : "", // asset type
                Path.GetExtension(entry.Name)); // extension

            // create folder structure
            var directory = Utils.CreateDirectory(outputDirectory,
                Section.Name.TrimStart('/'),
                entry.Flags.ToString());

            var path = Path.Combine(directory, filename);

            File.WriteAllBytes(path, entry.ReadData(stream));
        }

        [Obsolete("Test method")]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
        private void TestSave()
        {
            //Save(ArchivePath.Replace("Multiverse", "Multiverse - Copy"));

            Save("test.bin");

            using var md5 = MD5.Create();
            var h1 = BitConverter.ToString(md5.ComputeHash(File.ReadAllBytes(ArchivePath)));
            var h2 = BitConverter.ToString(md5.ComputeHash(File.ReadAllBytes("test.bin")));

            Debug.Assert(h1 == h2);
        }
    }
}
