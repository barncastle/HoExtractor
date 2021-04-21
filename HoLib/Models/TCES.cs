using HoLib.Helpers;
using System.Buffers.Binary;

namespace HoLib.Models
{
    public class TCES
    {
        public readonly Flags Flags;
        public readonly int Unknown2;
        public readonly int Unknown3;
        public readonly int Size;
        public readonly int Unknown5;
        public readonly int Unknown6;
        public readonly int PageOffset; // << 11, offset to section data
        public readonly int SectionSize;
        public readonly int SectionSize2;
        public readonly int Unknown10;
        public readonly int Unknown11;
        public readonly int Unknown12;
        public readonly int Unknown13;
        public readonly int Unknown14;
        public readonly int Unknown15;

        public TCES(EndianAwareBinaryReader reader)
        {
            Flags = reader.ReadUInt32();
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

            // flags are always BE
            if (!reader.IsBigEndian)
                Flags = BinaryPrimitives.ReverseEndianness(Flags);
        }
    }
}
