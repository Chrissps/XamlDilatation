using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamlDilatation
{
    public delegate bool IsChildrenPropertyDelegate(object obj, object value, out List<object> children);
    
    public class ChildrenPropertySetting
    {
        public readonly PropertyInfo PropertyInfo;
        
        public readonly Type ObjectType;
        
        public readonly bool IsChildrenPropertyFlag;

        private readonly IsChildrenPropertyDelegate _isChildrenProperty;

        public ChildrenPropertySetting(PropertyInfo propertyInfo, IsChildrenPropertyDelegate isChildrenProperty)
        {
            PropertyInfo = propertyInfo;
            ObjectType = propertyInfo.DeclaringType;
            _isChildrenProperty = isChildrenProperty;
        }
        
        public ChildrenPropertySetting(PropertyInfo propertyInfo, bool isChildrenProperty)
        {
            PropertyInfo = propertyInfo;
            ObjectType = propertyInfo.DeclaringType;
            IsChildrenPropertyFlag = isChildrenProperty;
        }

        public bool IsChildrenProperty(object obj, object value, out List<object> children)
        {
            children = null;
            return _isChildrenProperty?.Invoke(obj, value, out children) ?? IsChildrenPropertyFlag;
        }
    }
}