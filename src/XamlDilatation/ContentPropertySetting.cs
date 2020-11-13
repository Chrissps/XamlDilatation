using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamlDilatation
{
    public class ContentPropertySetting
    {
        public readonly PropertyKey PropertyInfo;
        
        public readonly Type ObjectType;
        
        public readonly bool IsContentPropertyFlag;

        private readonly Func<object, object, bool> _isContentProperty;

        public ContentPropertySetting(PropertyKey propertyKey, Func<object, object, bool> isContentProperty)
        {
            PropertyInfo = propertyKey;
            ObjectType = propertyKey.DeclaringType;
            _isContentProperty = isContentProperty;
        }
        
        public ContentPropertySetting(PropertyKey propertyKey, bool isContentProperty)
        {
            PropertyInfo = propertyKey;
            ObjectType = propertyKey.DeclaringType;
            IsContentPropertyFlag = isContentProperty;
        }
        
        public bool IsContentProperty(object obj, object value) =>
            _isContentProperty?.Invoke(obj, value) ?? IsContentPropertyFlag;
    }
}