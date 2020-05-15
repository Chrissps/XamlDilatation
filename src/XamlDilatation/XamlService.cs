using System;
using System.Collections.Generic;
// ReSharper disable CollectionNeverUpdated.Global

namespace XamlDilatation
{
    public class XamlService
    {
        public readonly Dictionary<string, XmlnsDeclaration> XmlnsDeclarations = new Dictionary<string, XmlnsDeclaration>();
        public readonly Dictionary<Type, ShouldSerializeSetting> ShouldSerializeSettings = new Dictionary<Type, ShouldSerializeSetting>();
        
        public XamlService()
        {
            Initialize();
        }

        private void Initialize()
        {
            
        }

        public void Serialize(object obj)
        {
            
        }
    }
}