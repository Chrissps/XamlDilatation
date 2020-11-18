namespace XamlDilatation.Settings
{
    public delegate bool ShouldSerializeDelegate<in TParent, in T>(TParent parentElement, T element);

    public class ShouldSerializeSetting
    {
        private readonly PropertyKey _propertyKey;

        private readonly bool _shouldSerializeFlag = true;
        
        private readonly ShouldSerializeDelegate<object, object> _shouldSerialize;

        protected ShouldSerializeSetting(PropertyKey propertyKey, ShouldSerializeDelegate<object, object> shouldSerialize)
        {
            _propertyKey = propertyKey;
            _shouldSerialize = shouldSerialize;
        }
        
        public ShouldSerializeSetting(PropertyKey propertyKey, bool shouldSerialize)
        {
            _propertyKey = propertyKey;
            _shouldSerializeFlag = shouldSerialize;
        }
        
        public bool ShouldSerialize(object parentElement, object element) =>
            _shouldSerialize?.Invoke(parentElement, element) ?? _shouldSerializeFlag;
    }
    
    public class ShouldSerializeSetting<TParent, T> : ShouldSerializeSetting
    {
        public ShouldSerializeSetting(PropertyKey propertyKey, ShouldSerializeDelegate<TParent, T> shouldSerialize) :
            base(propertyKey, (parentElement, element) => shouldSerialize((TParent) parentElement, (T) element))
        {
            
        }

        public ShouldSerializeSetting(PropertyKey propertyKey, bool shouldSerialize) : 
            base(propertyKey, shouldSerialize)
        {
            
        }
    }
}