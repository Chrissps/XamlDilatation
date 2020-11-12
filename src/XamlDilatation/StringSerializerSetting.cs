using System;

namespace XamlDilatation
{
    public class StringSerializerSetting
    {
        public readonly Type ObjectType;

        private readonly Func<object, object, bool> _shouldSerialize;

        private readonly Func<object, object, string> _serialize;

        public StringSerializerSetting(Type objectType, Func<object, object, bool> shouldSerialize, Func<object, object, string> serialize)
        {
            ObjectType = objectType;
            _shouldSerialize = shouldSerialize;
            _serialize = serialize;
        }

        public bool ShouldSerialize(object obj, object parentObj) =>
            _shouldSerialize?.Invoke(obj, parentObj) ?? true;
        
        public string Serialize(object obj, object parentObj) => _serialize?.Invoke(obj, parentObj);
    }
}