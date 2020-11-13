using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XamlDilatation
{
    public static class Extensions
    {
        public static PropertyInfo GetPublicPropertyInfo(this Type type, string name) =>
            type.GetPublicProperties(out _).First(o => o.Name == name);

        public static List<PropertyInfo> GetPublicProperties(this Type type, out List<Type> foundTypes)
        {
            const BindingFlags flags = BindingFlags.Public
                                       | BindingFlags.Instance;

            if (!type.IsInterface)
            {
                foundTypes = new List<Type> {type};
                return type.GetProperties(flags).ToList();
            }

            var propertyInfos = new List<PropertyInfo>();
            var considered = new List<Type>();
            var queue = new Queue<Type>();
            considered.Add(type);
            queue.Enqueue(type);
            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();
                foreach (var subInterface in subType.GetInterfaces())
                {
                    if (considered.Contains(subInterface)) continue;

                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }
                
                var typeProperties = subType.GetProperties(flags);
                var newPropertyInfos = typeProperties
                    .Where(x => !propertyInfos.Contains(x));

                propertyInfos.InsertRange(0, newPropertyInfos);
            }

            foundTypes = considered;
            return propertyInfos;
        }
    }
}