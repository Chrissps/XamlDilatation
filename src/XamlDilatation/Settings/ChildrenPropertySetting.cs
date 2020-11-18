using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamlDilatation.Settings
{
    public delegate bool ChildrenPropertyDelegate<in TParent, in T>(TParent parentElement, T element, out List<object> value);

    public class ChildrenPropertySetting
    {
        private readonly PropertyKey _propertyKey;
        
        private readonly ChildrenPropertyDelegate<object, object> _childrenProperty;

        protected ChildrenPropertySetting(PropertyKey propertyKey, ChildrenPropertyDelegate<object, object> childrenProperty)
        {
            _propertyKey = propertyKey;
            _childrenProperty = childrenProperty;
        }

        public bool ShouldChildrenProperty(object parentElement, object element, out List<object> value)
        {
            value = null;
            return _childrenProperty?.Invoke(parentElement, element, out value) ?? false;
        }
    }
    
    public class ChildrenPropertySetting<TParent, T> : ChildrenPropertySetting
    {
        public ChildrenPropertySetting(PropertyKey propertyKey, ChildrenPropertyDelegate<TParent, T> childrenProperty) :
            base(propertyKey, (object parentElement, object element, out List<object> value) => childrenProperty((TParent) parentElement, (T) element, out value))
        {
            
        }
    }
}