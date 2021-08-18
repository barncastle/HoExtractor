using HoLib.Helpers;
using HoLib.Static;

namespace HoLib.Models
{
    public class Layer : IModel
    {
        public readonly LayerType LayerType;
        public readonly Flags Flags; // unique to each layer
        public readonly int Index; // XETP blocks are split into multiple layers
        public readonly int TCES_Unknown3; // always matches TCES.Unknown3
        public readonly int Section_SizeOfData; // always matches Section.SizeOfData
        public readonly int Unknown5; // always 0
        public readonly int Unknown6; // always -1
        public int PageOffset; // << 11, offset to data 
        public int TotalLayerSize;
        public int TotalLayerSize2;
        public readonly int PageSize; // << 11, game page buffer size
        public readonly int Unknown11; // always 0
        public readonly int Unknown12; // always -1
        public readonly int Unknown13; // always 1
        public readonly int SubLayerOffset;
        public readonly int Unknown15; // always 0

        public Layer(EndianAwareBinaryReader reader)
        {
            LayerType = (LayerType)reader.ReadUInt32();
            Flags = reader.ReadFlags();
            Index = reader.ReadInt32();
            TCES_Unknown3 = reader.ReadInt32();
            Section_SizeOfData = reader.ReadInt32();
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
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.WriteInt32((int)LayerType);
            writer.WriteFlags(Flags);
            writer.WriteInt32(Index);
            writer.WriteInt32(TCES_Unknown3);
            writer.WriteInt32(Section_SizeOfData);
            writer.WriteInt32(Unknown5);
            writer.WriteInt32(Unknown6);
            writer.WriteInt32(PageOffset);
            writer.WriteInt32(TotalLayerSize);
            writer.WriteInt32(TotalLayerSize2);
            writer.WriteInt32(PageSize);
            writer.WriteInt32(Unknown11);
            writer.WriteInt32(Unknown12);
            writer.WriteInt32(Unknown13);
            writer.WriteInt32(SubLayerOffset);
            writer.WriteInt32(Unknown15);
        }
    }
}
