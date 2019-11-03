using System;

namespace shared.Extensions
{
    public static class ActionExtensions
    {
        public static Action<object> Convert<T>(this Action<T> action)
        {
            if (action == null) return null;
            return o => action((T) o);
        }

        public static Action<object> TryConvert<T>(this Action<T> action)
        {
            if (action == null) return null;
            return o =>
            {
                if (!(o is T)) o = default(T);
                action((T) o);
            };
        }

        public static Action<object, object> Convert<T1, T2>(this Action<T1, T2> action)
        {
            if (action == null) return null;
            return (o1, o2) => action((T1) o1, (T2) o2);
        }

        public static Action<object, object> TryConvert<T1, T2>(this Action<T1, T2> action)
        {
            if (action == null) return null;
            return (o1, o2) =>
            {
                if (!(o1 is T1)) o1 = default(T1);
                if (!(o2 is T2)) o2 = default(T2);
                action((T1) o1, (T2) o2);
            };
        }

        public static Action<object, object, object> Convert<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            if (action == null) return null;
            return (o1, o2, o3) => action((T1) o1, (T2) o2, (T3)o3);
        }

        public static Action<object, object, object> TryConvert<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            if (action == null) return null;
            return (o1, o2, o3) =>
            {
                if (!(o1 is T1)) o1 = default(T1);
                if (!(o2 is T2)) o2 = default(T2);
                if (!(o3 is T3)) o2 = default(T3);
                action((T1) o1, (T2) o2, (T3)o3);
            };
        }
    }
}
