using System;
using System.Collections.Generic;
using System.Reflection;
// ReSharper disable MemberCanBePrivate.Global

namespace XamlDilatation
{
    public class PropertyKey
    {
        private static readonly Dictionary<string, PropertyKey> _cache = new();
        
        public readonly Type DeclaringType;

        public readonly Type PropertyType;
        
        public readonly string Name;

        private PropertyKey(PropertyInfo info)
        {
            DeclaringType = info.DeclaringType;
            PropertyType = info.PropertyType;
            Name = info.Name;
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => BuildString(this);

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

        private static string BuildString(PropertyKey info) => 
            BuildString(info.DeclaringType, info.PropertyType, info.Name);

        public static PropertyKey Get(PropertyInfo info)
        {
            var key = BuildString(info);
            if (_cache.ContainsKey(key)) return _cache[key];
            
            _cache.Add(key, new PropertyKey(info));
            return _cache[key];
        }
    }
}