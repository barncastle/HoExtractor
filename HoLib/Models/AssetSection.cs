using HoLib.Helpers;
using HoLib.Static;

namespace HoLib.Models
{
    public class AssetSection : IModel
    {
        public readonly AssetSectionType Type;
        public int StartOffset;
        public int Size;
        public readonly int Alignment;

        public AssetSection(EndianAwareBinaryReader reader)
        {
            Type = (AssetSectionType)reader.ReadInt32();
            StartOffset = reader.ReadInt32();
            Size = reader.ReadInt32();
            Alignment = reader.ReadInt32();
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32((int)Type);
            writer.WriteInt32(StartOffset);
            writer.WriteInt32(Size);
            writer.WriteInt32(Alignment);
        }
    }
}
