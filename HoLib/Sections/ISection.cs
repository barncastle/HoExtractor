using HoLib.Helpers;

namespace HoLib.Sections
{
    public interface ISection
    {
        public void Write(EndianAwareBinaryWriter writer);
    }
}
