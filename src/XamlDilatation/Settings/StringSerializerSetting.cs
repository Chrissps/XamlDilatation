namespace XamlDilatation.Settings
{
    public delegate bool StringSerializerDelegate<in TParent, in T>(TParent parentElement, T element, out string value);

    public class StringSerializerSetting
    {
        private readonly PropertyKey _propertyKey;
        
        private readonly StringSerializerDelegate<object, object> _stringSerializer;

        protected StringSerializerSetting(PropertyKey propertyKey, StringSerializerDelegate<object, object> stringSerializer)
        {
            _propertyKey = propertyKey;
            _stringSerializer = stringSerializer;
        }

        public bool ShouldStringSerializer(object parentElement, object element, out string value)
        {
            value = null;
            return _stringSerializer?.Invoke(parentElement, element, out value) ?? false;
        }
    }
    
    public class StringSerializerSetting<TParent, T> : StringSerializerSetting
    {
        public StringSerializerSetting(PropertyKey propertyKey, StringSerializerDelegate<TParent, T> shouldSerialize) :
            base(propertyKey, (object parentElement, object element, out string value) => shouldSerialize((TParent) parentElement, (T) element, out value))
        {
            
        }
    }
}