using System;
using System.Collections.Generic;
using System.IO;
using server.Util.Serialization;

namespace server.Util.Extensions
{
    public static class BinaryWriterExtensions
    {

        public static void Write<TKey, TValue>(this BinaryWriter bw, Dictionary<TKey, TValue> dictionary)
        {
            bw.Write(server.Util.Serialization.Util.SerializeDictionary(dictionary));
        }

        public static void Write<T>(this BinaryWriter bw, Serializable<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(value.SerializeObject());
        }

        public static void Write<T>(this BinaryWriter bw, List<T> value)
            where T : Serializable<T>, new()
        {
            bw.Write(server.Util.Serialization.Util.SerializeListSerializable(value));
        }

        public static void Write(this BinaryWriter bw, List<IConvertible> value)
        {
            bw.Write(server.Util.Serialization.Util.SerializeListIConvertible(value));
        }

    }
}
