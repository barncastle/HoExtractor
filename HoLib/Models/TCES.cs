using HoLib.Helpers;
using HoLib.Static;

namespace HoLib.Models
{
    public class TCES : IModel
    {
        public readonly BlockType Tag;
        public readonly Flags Flags;
        public readonly int Unknown2; // always 0
        public readonly int Unknown3; // flags of some kind?
        public readonly int Size;
        public readonly int Unknown5; // always 0
        public readonly int Unknown6; // always -1
        public readonly int PageOffset; // << 11, offset to section data
        public readonly int SectionSize;
        public readonly int SectionSize2;
        public readonly int Unknown10; // always -1
        public readonly int Unknown11; // always 0
        public readonly int Unknown12; // always -1
        public readonly int Unknown13; // always 0
        public readonly int Unknown14; // always -1
        public readonly int Unknown15; // always 0

        public TCES(EndianAwareBinaryReader reader)
        {
            // this is reversed to signify this is the section header
            // for multi-sectioned files (which don't exist)
            Tag = reader.AssertTag(BlockType.TCES, BlockType.SECT);

            Flags = reader.ReadFlags();
            Unknown2 = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Size = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            PageOffset = reader.ReadInt32(); // << 11
            SectionSize = reader.ReadInt32();
            SectionSize2 = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown11 = reader.ReadInt32();
            Unknown12 = reader.ReadInt32();
            Unknown13 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Unknown15 = reader.ReadInt32();
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteTag(Tag);
            writer.WriteFlags(Flags);
            writer.WriteInt32(Unknown2);
            writer.WriteInt32(Unknown3);
            writer.WriteInt32(Size);
            writer.WriteInt32(Unknown5);
            writer.WriteInt32(Unknown6);
            writer.WriteInt32(PageOffset);
            writer.WriteInt32(SectionSize);
            writer.WriteInt32(SectionSize2);
            writer.WriteInt32(Unknown10);
            writer.WriteInt32(Unknown11);
            writer.WriteInt32(Unknown12);
            writer.WriteInt32(Unknown13);
            writer.WriteInt32(Unknown14);
            writer.WriteInt32(Unknown15);
        }
    }
}
