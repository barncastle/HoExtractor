namespace HoLib.Static
{
    public enum BlockType : uint
    {
        // headers
        HEL = 0x1A4C4548,
        HEB = 0x1A424548,

        // master block
        MAST = 0x5453414D,

        // reversed section header
        TCES = 0x53454354,
        SECT = 0x54434553,
    }
}
