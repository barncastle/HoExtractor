using HoLib.Sections;
using System;
using System.ComponentModel;

namespace HoExtractor
{
    public static class Utils
    {
        public static string Stringify(AssetEntry entry, PropertyDescriptorCollection properties)
        {
            var value = "";
            foreach (PropertyDescriptor prop in properties)
            {
                var obj = prop.GetValue(entry);

                value += prop.DisplayName switch
                {
                    "AssetID" => ((ulong)obj).ToString("X16"),
                    "Flags" => ((uint)obj).ToString("X8"),
                    _ => obj.ToString(),
                };

                value += '|';
            }

            return value[..^1].ToUpperInvariant();
        }

        public static string EnumFormatter<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(value))
                return value.ToString();
            else
                return $"0x{Convert.ToUInt64(value):X}";
        }
    }
}
