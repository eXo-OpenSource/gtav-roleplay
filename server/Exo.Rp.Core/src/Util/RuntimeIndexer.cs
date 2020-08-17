using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using server.Util.Log;

namespace server.Util
{
    public class RuntimeIndexer : IService
    {
        private static readonly Logger<RuntimeIndexer> Logger = new Logger<RuntimeIndexer>();

        public void IndexWithAttribute<TAttribute, TMemberInfo>(Assembly target, Predicate<TMemberInfo> filter, Action<(TAttribute attribute, TMemberInfo memberInfo)> onFound)
            where TAttribute : Attribute
            where TMemberInfo : MemberInfo
        {
            foreach (var types in target.GetTypes())
            {
                var memberInfos = new List<TMemberInfo>();

                var type = typeof(TMemberInfo);
                if (type == typeof(MethodInfo))
                {
                    memberInfos.AddRange(types.GetMethods().Cast<TMemberInfo>());
                }
                else if (type == typeof(FieldInfo))
                {
                    memberInfos.AddRange(types.GetFields().Cast<TMemberInfo>());
                }
                else
                {
                    throw new NotSupportedException($"AttributeTarget {type} is not supported yet.");
                }
                memberInfos.RemoveAll(x => !filter(x));

                foreach (var memberInfo in memberInfos)
                {
                    foreach (var attribute in memberInfo.GetCustomAttributes<TAttribute>())
                    {
                        onFound((attribute, memberInfo));
                    }
                }
            }
        }

        public void IndexImplementsInterface<TInterface>(Assembly target, Action<Type> onFound)
        {
            foreach (var type in target.GetTypes())
            {
                if (type.GetInterface(typeof(TInterface).Name, true) != default)
                {
                    onFound(type);
                }
            }
        }
    }
}