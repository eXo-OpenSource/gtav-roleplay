using System;
using System.Collections.Generic;
using System.IO;
using shared.Serialization;

namespace shared.Extensions
{
    public static class BinaryWriterExtensions
    {

        public static void Write<TKey, TValue>(this BinaryWriter bw, Dictionary<TKey, TValue> dictonary)
        {
            bw.Write(Util.SerializeDictionary(dictonary));
        }

        public static void Write<T>(this BinaryWriter bw, Serializable<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(value.SerializeObject());
        }

        public static void Write<T>(this BinaryWriter bw, List<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(Util.SerializeListSerializable(value));
        }

        public static void Write(this BinaryWriter bw, List<IConvertible> value)
        {
            bw.Write(Util.SerializeListIConvertible(value));
        }

    }
}
