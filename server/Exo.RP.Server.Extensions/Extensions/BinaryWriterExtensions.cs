using System;
using System.Collections.Generic;
using System.IO;
using extensions.Util.Serialization;

namespace extensions.Extensions
{
    public static class BinaryWriterExtensions
    {

        public static void Write<TKey, TValue>(this BinaryWriter bw, Dictionary<TKey, TValue> dictionary)
        {
            bw.Write(Util.Serialization.Util.SerializeDictionary(dictionary));
        }

        public static void Write<T>(this BinaryWriter bw, Serializable<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(value.SerializeObject());
        }

        public static void Write<T>(this BinaryWriter bw, List<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(Util.Serialization.Util.SerializeListSerializable(value));
        }

        public static void Write(this BinaryWriter bw, List<IConvertible> value)
        {
            bw.Write(Util.Serialization.Util.SerializeListIConvertible(value));
        }

    }
}
