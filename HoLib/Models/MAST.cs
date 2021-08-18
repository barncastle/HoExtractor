using HoLib.Helpers;
using HoLib.Static;

namespace HoLib.Models
{
    public class MAST : IModel
    {
        public readonly BlockType Tag;
        public readonly int PageSize; // << 11, size of current block inc padding
        public readonly int Unknown2; // always 0
        public readonly int StringTableOffset;
        public readonly int StringTableSize;
        public readonly int Unknown5; // always -1
        public readonly int Unknown6; // always 0
        public readonly int Unknown7; // always 0

        public MAST(EndianAwareBinaryReader reader)
        {
            Tag = reader.AssertTag(BlockType.MAST);
            PageSize = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            StringTableOffset = reader.ReadInt32();
            StringTableSize = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            Unknown7 = reader.ReadInt32();
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteTag(Tag);
            writer.WriteInt32(PageSize);
            writer.WriteInt32(Unknown2);
            writer.WriteInt32(StringTableOffset);
            writer.WriteInt32(StringTableSize);
            writer.WriteInt32(Unknown5);
            writer.WriteInt32(Unknown6);
            writer.WriteInt32(Unknown7);
        }
    }
}
