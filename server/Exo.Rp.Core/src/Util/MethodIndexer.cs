using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace server.Util
{
    public class MethodIndexer
    {
        public void IndexWithAttribute<TMemberInfoType, TAttribute>(Assembly target, AttributeTargets attributeTarget, Predicate<TMemberInfoType> filter, Action<(TAttribute attribute, TMemberInfoType memberInfo)> onFound)
            where TMemberInfoType : MemberInfo
            where TAttribute : Attribute
        {
            foreach (var types in target.GetTypes())
            {
                var memberInfos = new List<TMemberInfoType>();
                switch (attributeTarget)
                {
                    case AttributeTargets.Method:
                        memberInfos.AddRange(types.GetMethods().Cast<TMemberInfoType>());
                        break;
                    case AttributeTargets.Field:
                        memberInfos.AddRange(types.GetFields().Cast<TMemberInfoType>());
                        break;
                    case AttributeTargets.Assembly:
                    case AttributeTargets.Module:
                    case AttributeTargets.Struct:
                    case AttributeTargets.Enum:
                    case AttributeTargets.Constructor:
                    case AttributeTargets.Property:
                    case AttributeTargets.Event:
                    case AttributeTargets.Interface:
                    case AttributeTargets.Parameter:
                    case AttributeTargets.Delegate:
                    case AttributeTargets.ReturnValue:
                    case AttributeTargets.GenericParameter:
                    case AttributeTargets.All:
                        throw new NotSupportedException($"AttributeTarget {attributeTarget} is not supported yet.");
                }
                memberInfos.RemoveAll(x => !filter(x));

                foreach (var memberInfo in memberInfos)
                {
                    foreach (var attribute in memberInfo.GetCustomAttributes())
                    {
                        if (typeof(TAttribute) == attribute.GetType())
                        {
                            onFound((attribute as TAttribute, memberInfo as TMemberInfoType));
                        }
                    }
                }
            }
        }
    }
}