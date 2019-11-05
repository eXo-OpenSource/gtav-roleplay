using System;
using System.Collections.Generic;
using System.Linq;

namespace extensions.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> MergeLeft<TKey, TValue>(this Dictionary<TKey, TValue> left,
            Dictionary<TKey, TValue> right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            var merged = new Dictionary<TKey, TValue>();
            left.ToList().ForEach(kv => merged[kv.Key] = kv.Value);
            right.ToList().ForEach(kv => merged[kv.Key] = kv.Value);

            return merged;
        }

        public static Dictionary<TKey, TValue> MergeLeft<TKey, TValue>(
            this Dictionary<TKey, TValue> left, Dictionary<TKey, TValue> right,
            Action<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>> merge)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            var merged = new Dictionary<TKey, TValue>();
            merge.Invoke(left, merged);
            merge.Invoke(right, merged);

            return merged;
        }

        public static Dictionary<TKey, TValue> MergeRight<TKey, TValue>(this Dictionary<TKey, TValue> left, Dictionary<TKey, TValue> right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return right.MergeLeft(left);
        }

        public static Dictionary<TKey, TValue> MergeRight<TKey, TValue>(
            this Dictionary<TKey, TValue> left, Dictionary<TKey, TValue> right,
            Action<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>> merge)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return right.MergeLeft(left, merge);
        }
    }
}