using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamlDilatation
{
    public class PropertyKey
    {
        public static readonly Dictionary<string, PropertyKey> Cache = new Dictionary<string, PropertyKey>();
        
        public readonly Type DeclaringType;

        public readonly Type PropertyType;

        public readonly string PropertyName;

        public PropertyKey(Type declaringType, Type propertyType, string propertyName)
        {
            DeclaringType = declaringType;
            PropertyType = propertyType;
            PropertyName = propertyName;
        }
        
        public PropertyKey(PropertyInfo info)
        {
            DeclaringType = info.DeclaringType;
            PropertyType = info.PropertyType;
            PropertyName = info.Name;
        }

        public override int GetHashCode() => BuildString(DeclaringType, PropertyType, PropertyName).GetHashCode();

        public override string ToString() => BuildString(DeclaringType, PropertyType, PropertyName);

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj is null) return false;
            return obj is PropertyKey pk && pk.GetHashCode() == GetHashCode();
        }

        private static string BuildString(Type declaringType, Type propertyType, string propertyName) =>
            $"{declaringType.FullName}{propertyType.FullName}{propertyName}";

        private static string BuildString(PropertyInfo info) =>
            BuildString(info.DeclaringType, info.PropertyType, info.Name);

        public static PropertyKey Get(PropertyInfo info)
        {
            var key = BuildString(info);
            if (Cache.ContainsKey(key)) return Cache[key];
            
            Cache.Add(key, new PropertyKey(info));
            return Cache[key];
        }
    }
}