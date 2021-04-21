namespace HoLib.Models
{
    /// <summary>
    /// Used by the archive system to group files together
    /// by reqirement e.g. localised files will all have the same flags
    /// </summary>
    public struct Flags
    {
        public byte TCES => (byte)(RawValue & 0xFF);
        public byte Section => (byte)((RawValue >> 8) & 0xFF);
        public byte Unknown1 => (byte)((RawValue >> 16) & 0xFF);
        public byte Unknown2 => (byte)((RawValue >> 24) & 0xFF); // probably locale?

        private readonly uint RawValue;

        public Flags(uint value) => RawValue = value;

        public override bool Equals(object obj) => obj is Flags other && other.RawValue == RawValue;

        public static bool operator ==(Flags left, Flags right) => left.Equals(right);

        public static bool operator !=(Flags left, Flags right) => !(left == right);

        public override int GetHashCode() => RawValue.GetHashCode();

        public static implicit operator uint(Flags flags) => flags.RawValue;

        public static implicit operator Flags(uint value) => new(value);

        public override string ToString() => RawValue.ToString("X8");
    }
}
