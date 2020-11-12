using System;

namespace XamlDilatation
{
    public class ShouldSerializerSetting
    {
        public readonly Type ObjectType;
        
        public readonly Type ParentObjectType;
        
        public readonly bool? ShouldSerializeFlag;

        private readonly Func<object, object, bool> _shouldSerialize;

        public ShouldSerializerSetting(Type objectType, Func<object, object, bool> shouldSerialize)
        {
            ObjectType = objectType;
            _shouldSerialize = shouldSerialize;
        }
        
        public ShouldSerializerSetting(Type objectType, bool shouldSerialize)
        {
            ObjectType = objectType;
            ShouldSerializeFlag = shouldSerialize;
        }
        
        public ShouldSerializerSetting(Type objectType, Type parentObjectType, bool shouldSerialize)
        {
            ObjectType = objectType;
            ParentObjectType = parentObjectType;
            ShouldSerializeFlag = shouldSerialize;
        }

        public bool ShouldSerialize(object obj, object parentObj) =>
            _shouldSerialize?.Invoke(obj, parentObj) ?? true;
    }
}