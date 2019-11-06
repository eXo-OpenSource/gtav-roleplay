using AltV.Net.Data;

namespace server.Extensions
{
    internal static class RgbaExtensions
    {
        public static int ToInt32(this Rgba rgba)
        {
            var r = rgba.R & 0xFF;
            var g = rgba.G & 0xFF;
            var b = rgba.B & 0xFF;
            var a = rgba.A & 0xFF;
            return (r << 24) | (g << 16) | (b << 8) | a;
        }
    }
}
