using System;
using System.Linq;
using System.Reflection;

namespace server.Util
{
    public class MethodIndexer
    {
        public void IndexMethodsWithAttribute<TAttribute>(Assembly target, Action<(TAttribute attribute, MethodInfo method)> onFound)
            where TAttribute : Attribute
        {
            foreach (var types in target.GetTypes())
            {
                foreach (var method in types.GetMethods().Where(m => m.IsPublic && m.IsStatic))
                {
                    foreach (var attribute in method.GetCustomAttributes())
                    {
                        if (typeof(TAttribute) == attribute.GetType())
                        {
                            onFound((attribute as TAttribute, method));
                        }
                    }
                }
            }
        }
    }
}