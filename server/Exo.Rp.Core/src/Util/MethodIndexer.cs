using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using server.Util.Log;

namespace server.Util
{
    public class MethodIndexer : IService
    {
        private static readonly Logger<MethodIndexer> Logger = new Logger<MethodIndexer>();

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
        
        public void IndexImplementsInterface<TInterface>(Assembly target, Predicate<Type> filter, Action<Type> onFound)
        {
            foreach (var type in target.GetTypes())
            {
                foreach (var memberInfo in type.GetInterfaces())
                {
                    if (memberInfo == typeof(TInterface))
                    {
                        onFound(type);
                    }
                }
            }
        }

        public void IndexMethods(Assembly target, string methodName, Predicate<MethodInfo> filter, Action<MethodInfo> onFound)
        {
            foreach (var type in target.GetTypes())
            {
                var method = type.GetMethod(methodName);
                if (method != default && filter(method))
                {
                    onFound(method);
                }
            }
        }
    }
}