namespace HoLib.Static
{
    public enum AssetSectionType : uint
    {
        /// <summary>
        /// Contains multiple asset entries
        /// </summary>
        AssetGroup = 0,
        /// <summary>
        /// Contains raw asset data
        /// </summary>
        AssetData = 1,
        /// <summary>
        /// Alignment padding
        /// </summary>
        Padding = 2
    }
}
