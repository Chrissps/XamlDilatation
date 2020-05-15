using System;
using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace XamlDilatation
{
    public class ShouldSerializeSetting
    {
        public Type ObjectType { get; private set; }
        public Func<object, bool> ShouldSerialize { get; private set; }
        
        public Dictionary<Type, Func<object, object, bool>> ParentTypes { get; } = new Dictionary<Type,Func<object, object, bool>>();

        public ShouldSerializeSetting() { }

        public void Register<T>(Func<T, bool> shouldSerialize)
        {
            ObjectType = typeof(T);
            ShouldSerialize = a => shouldSerialize((T)a);
        }
        
        public void Register<T, TParent>(Func<T, TParent, bool> shouldSerialize)
        {
            ObjectType = typeof(T);

            var parentType = typeof(TParent);
            // ReSharper disable once ConvertToLocalFunction
            Func<object, object, bool> newFunc = (a, b) => shouldSerialize((T) a, (TParent) b);
            
            if (ParentTypes.ContainsKey(parentType))
                ParentTypes[parentType] = newFunc;
            else
                ParentTypes.Add(parentType, newFunc);
        }
    }
}