using System;
using System.Collections.Generic;

namespace XamlDilatation.Settings
{
    public delegate bool ContentPropertyDelegate<in TParent, in T>(TParent parentElement, T element);

    public class ContentPropertySetting
    {
        private readonly PropertyKey _propertyKey;

        private readonly bool _contentPropertyFlag;
        
        private readonly ContentPropertyDelegate<object, object> _contentProperty;

        protected ContentPropertySetting(PropertyKey propertyKey, ContentPropertyDelegate<object, object> contentProperty)
        {
            _propertyKey = propertyKey;
            _contentProperty = contentProperty;
        }
        
        public ContentPropertySetting(PropertyKey propertyKey, bool contentProperty)
        {
            _propertyKey = propertyKey;
            _contentPropertyFlag = contentProperty;
        }
        
        public bool ShouldSerialize(object parentElement, object element) =>
            _contentProperty?.Invoke(parentElement, element) ?? _contentPropertyFlag;
    }
    
    public class ContentPropertySetting<TParent, T> : ContentPropertySetting
    {
        public ContentPropertySetting(PropertyKey propertyKey, ContentPropertyDelegate<TParent, T> contentProperty) :
            base(propertyKey, (parentElement, element) => contentProperty((TParent) parentElement, (T) element))
        {
            
        }

        public ContentPropertySetting(PropertyKey propertyKey, bool contentProperty) : 
            base(propertyKey, contentProperty)
        {
            
        }
    }
}