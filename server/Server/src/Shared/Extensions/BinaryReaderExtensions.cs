using System;
using System.Collections.Generic;
using System.IO;
using shared.Serialization;

namespace shared.Extensions
{
    public static class BinaryReaderExtensions
    {

        public static Dictionary<TKey, TValue> ReadDictionarySerializable<TKey, TValue>(this BinaryReader br)
            where TKey : IConvertible
            where TValue : Serializable<TValue>, new()
        {
            return Util.DeserializeDictionarySerializable<TKey, TValue>(br.ReadString());
        }

        public static Dictionary<TKey, TValue> ReadDictionaryIConvertible<TKey, TValue>(this BinaryReader br)
            where TKey : IConvertible
            where TValue : IConvertible
        {
            return Util.DeserializeDictionaryIConvertible<TKey, TValue>(br.ReadString());
        }

        public static List<T> ReadListSerializable<T>(this BinaryReader br)
            where T : Serializable<T>, new()
        {
            return Util.DeserializeListSerializable<T>(br.ReadString());
        }

        public static List<T> ReadListIConvertible<T>(this BinaryReader br)
            where T : IConvertible
        {
            return Util.DeserializeListIConvertible<T>(br.ReadString());
        }

        public static T ReadSerializable<T>(this BinaryReader br)
            where T : Serializable<T>, new()
        {
            return new T().DeserializeObject(br.ReadString());
        }

    }
}
