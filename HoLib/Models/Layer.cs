using HoLib.Helpers;
using HoLib.Static;
using System.Buffers.Binary;

namespace HoLib.Models
{
    public class Layer
    {
        public readonly LayerType LayerType;
        public readonly Flags Flags; // unique to each layer
        public readonly int Unknown2;
        public readonly int Unknown3;
        public readonly int Unknown4;
        public readonly int Unknown5;
        public readonly int Unknown6;
        public readonly int PageOffset; // << 11, offset to data 
        public readonly int TotalLayerSize;
        public readonly int TotalLayerSize2;
        public readonly int PageSize; // << 11, game page buffer size
        public readonly int Unknown11;
        public readonly int Unknown12;
        public readonly int Unknown13;
        public readonly int SubLayerOffset;
        public readonly int Unknown15;

        public Layer(EndianAwareBinaryReader reader)
        {
            LayerType = (LayerType)reader.ReadUInt32();
            Flags = reader.ReadUInt32();
            Unknown2 = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6 = reader.ReadInt32();
            PageOffset = reader.ReadInt32();
            TotalLayerSize = reader.ReadInt32();
            TotalLayerSize2 = reader.ReadInt32();
            PageSize = reader.ReadInt32();
            Unknown11 = reader.ReadInt32();
            Unknown12 = reader.ReadInt32();
            Unknown13 = reader.ReadInt32();
            SubLayerOffset = reader.ReadInt32();
            Unknown15 = reader.ReadInt32();

            // flags are always BE
            if (!reader.IsBigEndian)
                Flags = BinaryPrimitives.ReverseEndianness(Flags);
        }
    }
}
