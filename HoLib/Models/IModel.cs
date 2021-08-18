using HoLib.Helpers;

namespace HoLib.Models
{
    public interface IModel
    {
        public void Write(EndianAwareBinaryWriter writer);
    }
}
