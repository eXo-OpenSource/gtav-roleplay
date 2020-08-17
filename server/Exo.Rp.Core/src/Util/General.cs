using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AltV.Net.Data;

namespace server.Util
{
    internal static class General
    {
        public static string Sha256(string String)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(String), 0, Encoding.UTF8.GetByteCount(String));
            foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public static List<int> SerializeColor(Rgba color)
        {
            return new List<int> {color.R, color.G, color.B};
        }

        public static Rgba DeSerializeColor(List<byte> color)
        {
            return new Rgba(color[0], color[1], color[2], 255);
        }

        public static Rgba GetRandomColor()
        {
            var rnd = new Random();
            return new Rgba((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255), 255);
        }
    }
}