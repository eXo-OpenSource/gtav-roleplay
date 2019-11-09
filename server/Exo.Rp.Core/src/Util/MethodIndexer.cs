using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace server.Util
{
    public class MethodIndexer
    {
        public void IndexWithAttribute<TAttribute, TMemberInfoType>(Assembly target, Predicate<TMemberInfoType> filter, Action<(TAttribute attribute, TMemberInfoType memberInfo)> onFound)
            where TAttribute : Attribute
            where TMemberInfoType : MemberInfo
        {
            foreach (var types in target.GetTypes())
            {
                var memberInfos = new List<TMemberInfoType>();

                var type = typeof(TMemberInfoType);
                if (type == typeof(MethodInfo))
                {
                    memberInfos.AddRange(types.GetMethods().Cast<TMemberInfoType>());
                }
                else if (type == typeof(FieldInfo))
                {
                    memberInfos.AddRange(types.GetFields().Cast<TMemberInfoType>());
                }
                else
                {
                    throw new NotSupportedException($"AttributeTarget {type} is not supported yet.");
                }
                memberInfos.RemoveAll(x => !filter(x));

                foreach (var memberInfo in memberInfos)
                {
                    foreach (var attribute in memberInfo.GetCustomAttributes())
                    {
                        if (typeof(TAttribute) == attribute.GetType())
                        {
                            onFound((attribute as TAttribute, memberInfo));
                        }
                    }
                }
            }
        }
    }
}