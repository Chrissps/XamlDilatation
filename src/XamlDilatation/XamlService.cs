using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

// ReSharper disable CollectionNeverUpdated.Global

namespace XamlDilatation
{
    public class XamlService
    {
        public readonly Dictionary<string, XmlnsDeclaration> XmlnsDeclarations = new Dictionary<string, XmlnsDeclaration>();
        
        public readonly Dictionary<Type, StringSerializerSetting> StringSerializerSettings = new Dictionary<Type, StringSerializerSetting>();
        public readonly Dictionary<PropertyInfo, ShouldSerializeSetting> ShouldSerializeSettings = new Dictionary<PropertyInfo, ShouldSerializeSetting>();
        public readonly Dictionary<PropertyKey, ContentPropertySetting> ContentPropertySettings = new Dictionary<PropertyKey, ContentPropertySetting>();
        public readonly Dictionary<PropertyInfo, ChildrenPropertySetting> ChildrenPropertySettings = new Dictionary<PropertyInfo, ChildrenPropertySetting>();
        
        public XamlService()
        {
            
        }

        public void Serialize(object obj)
        {
            var element = new DomElement(null, obj, this);
            element.Rebuild();
            // do serializeLogic
        }

        public XamlService RegisterDefault()
        {
            this.RegisterStringSerializer<short>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<int>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<long>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<ushort>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<uint>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<ulong>(arg1 => arg1.ToString());
            this.RegisterStringSerializer<float>(arg1 => arg1.ToString(CultureInfo.InvariantCulture));
            this.RegisterStringSerializer<double>(arg1 => arg1.ToString(CultureInfo.InvariantCulture));
            this.RegisterStringSerializer<decimal>(arg1 => arg1.ToString(CultureInfo.InvariantCulture));
            
            return this;
        }
    }
}