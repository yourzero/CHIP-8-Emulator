using System.Globalization;

namespace CHIP_8.Emulator.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Extracts a nibble from a large number.
        /// </summary>
        /// <typeparam name="T">Any integer type.</typeparam>
        /// <param name="t">The value to extract nibble from.</param>
        /// <param name="nibblePos">The nibble to check,
        /// where 0 is the least significant nibble.</param>
        /// <returns>The extracted nibble.</returns>
        public static byte GetNibble<T>(this T t, int nibblePos)
         where T : struct, IConvertible
        {
            nibblePos *= 4;
            var value = t.ToInt64(CultureInfo.CurrentCulture);
            return (byte)((value >> nibblePos) & 0xF);
        }


        public static (byte, byte) GetNibbles(this byte b)
        {
            // e.g.,
            //byte x = 0x12; //hexadecimal notation for decimal 18 or binary 0001 0010

            byte highNibble = (byte)(b >> 4 & 0xF); // = 0000 0001
            byte lowNibble = (byte)(b & 0xF); // = 0000 0010

            //            Console.WriteLine($" -- GetNibbles: b = {b.ToHex()}, highNibble: {highNibble.ToHex()}, lowNibble: {lowNibble.ToHex()}");

            return (highNibble, lowNibble);
        }

        public static string ToHex(this byte[] bytes)
        {
            return Convert.ToHexString(bytes);
        }

        public static string ToHex(this byte b)
        {
            return Convert.ToHexString(new[] { b });
        }

        public static string ToHex(this ushort b)
        {
            return ToHex((byte)b);

        }

    }

}
