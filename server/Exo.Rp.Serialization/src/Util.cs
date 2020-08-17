using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace serialization
{
    public static class Util
    {
        public static string SerializeDictionary<TKey, TSource>(Dictionary<TKey, TSource> dictionary)
        {
            var builder = new StringBuilder();
            foreach (var (key, value) in dictionary)
            {
                builder.Append(key).Append(":").Append(value).Append(',');
            }
            var result = builder.ToString();
            result = result.TrimEnd(',');
            return result;
        }

        public static Dictionary<TKey, TValue> DeserializeDictionaryIConvertible<TKey, TValue>(string dictonary)
            where TKey : IConvertible
            where TValue : IConvertible
        {
            var d = new Dictionary<TKey, TValue>();
            var tokens = dictonary.Split(new[] { ':', ',' },
                StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i += 2)
            {
                var key = (TKey)Convert.ChangeType(tokens[i], typeof(TKey));
                var value = (TValue)Convert.ChangeType(tokens[i + 1], typeof(TValue));
                /*
                if (d.ContainsKey(key))
                {
                    throw new ArgumentException("");
                }
                */
                d.Add(key, value);
            }
            return d;
        }

        public static Dictionary<TKey, TValue> DeserializeDictionarySerializable<TKey, TValue>(string dictonary)
            where TKey : IConvertible
            where TValue : Serializable<TValue>, new()
        {
            var d = new Dictionary<TKey, TValue>();
            var tokens = dictonary.Split(new[] { ':', ',' },
                StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i += 2)
            {
                var key = (TKey)Convert.ChangeType(tokens[i], typeof(TKey));
                var value = new TValue().DeserializeObject(tokens[i + 1]);
                /*
                if (d.ContainsKey(key))
                {
                    throw new ArgumentException("");
                }
                */
                d.Add(key, value);
            }
            return d;
        }

        public static string SerializeListSerializable<T>(List<T> list)
            where T : Serializable<T>, new()
        {
            return string.Join(":", list.ConvertAll(x => x.SerializeObject()));
        }

        public static string SerializeListIConvertible<T>(List<T> list)
            where T : IConvertible
        {
            return string.Join("|#|", list);
        }

        public static List<T> DeserializeListSerializable<T>(string value)
            where T : Serializable<T>, new()
        {
            return value.Split(":").ToList().ConvertAll(new T().DeserializeObject);
        }

        public static List<T> DeserializeListIConvertible<T>(string value)
            where T : IConvertible
        {
            return value.Split("|#|").ToList().ConvertAll(x => (T)Convert.ChangeType(x, typeof(T)));
        }
    }
}