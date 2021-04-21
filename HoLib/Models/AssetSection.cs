using HoLib.Helpers;
using HoLib.Static;

namespace HoLib.Models
{
    public class AssetSection
    {
        public AssetSectionType Type;
        public int StartOffset;
        public int Size;
        public int Unknown1; // only used by padding section with 0 size?

        public AssetSection(EndianAwareBinaryReader reader)
        {
            Type = (AssetSectionType)reader.ReadInt32();
            StartOffset = reader.ReadInt32();
            Size = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
        }
    }
}
