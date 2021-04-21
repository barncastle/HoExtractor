using HoLib.Helpers;
using System.IO;

namespace HoLib.Models
{
    public class AssetGroup
    {
        public readonly int Count;
        public readonly int Unknown1;

        public AssetGroup(EndianAwareBinaryReader reader)
        {
            Count = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            reader.Seek(0x18, SeekOrigin.Current); // padding
        }
    }
}
