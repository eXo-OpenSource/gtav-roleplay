using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Exo.Rp.Core.Util
{
    public class RuntimeIndexer : IService
    {
        public IEnumerable<(TAttribute attribute, TMemberInfo memberInfo)> IndexWithAttribute<TAttribute, TMemberInfo>(Assembly target, Predicate<TMemberInfo> filter)
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
                        yield return (attribute, memberInfo);
                    }
                }
            }
        }
        
        public IEnumerable<Type> IndexImplementsInterface<TInterface>(Assembly target)
        {
            return target
                .GetTypes()
                .Where(type => type.GetInterface(typeof(TInterface).Name, true) != default);
        }

        public IEnumerable<Type> IndexImplementsInterface<TInterface, TInterface2>(Assembly target)
        {
            return target
                .GetTypes()
                .Where(type => type.GetInterface(typeof(TInterface).Name, true) != default)
                .Where(type => type.GetInterface(typeof(TInterface2).Name, true) != default);
        }

        public IEnumerable<Type> IndexImplementsInterface<TInterface>(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterface(typeof(TInterface).Name, true) != default);
        }
    }
}