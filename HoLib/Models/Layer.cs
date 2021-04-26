using HoLib.Helpers;
using HoLib.Static;
using System.Buffers.Binary;

namespace HoLib.Models
{
    public class Layer
    {
        public readonly LayerType LayerType;
        public readonly Flags Flags; // unique to each layer
        public readonly int Index; // XETP blocks are split into multiple layers
        public readonly int TCES_Unknown3; // always matches TCES.Unknown3
        public readonly int Unknown4; // TODO size of something?
        public readonly int Unknown5; // always 0
        public readonly int Unknown6; // always -1
        public readonly int PageOffset; // << 11, offset to data 
        public readonly int TotalLayerSize;
        public readonly int TotalLayerSize2;
        public readonly int PageSize; // << 11, game page buffer size
        public readonly int Unknown11; // always 0
        public readonly int Unknown12; // always -1
        public readonly int Unknown13; // always 1
        public readonly int SubLayerOffset;
        public readonly int Unknown15; // always 0

        public Layer(EndianAwareBinaryReader reader)
        {
            LayerType = (LayerType)reader.ReadUInt32();
            Flags = reader.ReadUInt32();
            Index = reader.ReadInt32();
            TCES_Unknown3 = reader.ReadInt32();
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
