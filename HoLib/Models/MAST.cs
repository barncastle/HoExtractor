using HoLib.Helpers;

namespace HoLib.Models
{
    public class MAST
    {
        public readonly int PageSize; // << 11, size of current block inc padding
        public readonly int Unknown2;
        public readonly int StringTableOffset;
        public readonly int StringTableSize;
        public readonly int Unknown5;
        public readonly int Unknown6;
        public readonly int Unknown7;

        public MAST(EndianAwareBinaryReader reader)
        {
            PageSize = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            StringTableOffset = reader.ReadInt32();
            StringTableSize = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            Unknown7 = reader.ReadInt32();
        }
    }
}
