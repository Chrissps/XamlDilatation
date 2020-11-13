using System;
using System.Reflection;

namespace XamlDilatation
{
    public class ShouldSerializeSetting
    {
        public readonly PropertyInfo PropertyInfo;
        
        public readonly Type ObjectType;
        
        public readonly bool ShouldSerializeFlag = true;

        private readonly Func<object, object, bool> _shouldSerialize;

        public ShouldSerializeSetting(PropertyInfo propertyInfo, Func<object, object, bool> shouldSerialize)
        {
            PropertyInfo = propertyInfo;
            ObjectType = propertyInfo.DeclaringType;
            _shouldSerialize = shouldSerialize;
        }
        
        public ShouldSerializeSetting(PropertyInfo propertyInfo, bool shouldSerialize)
        {
            PropertyInfo = propertyInfo;
            ObjectType = propertyInfo.DeclaringType;
            ShouldSerializeFlag = shouldSerialize;
        }
        
        public bool ShouldSerialize(object obj, object value) =>
            _shouldSerialize?.Invoke(obj, value) ?? ShouldSerializeFlag;
    }
}