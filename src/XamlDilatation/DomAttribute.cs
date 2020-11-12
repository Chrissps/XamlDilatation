using System;
using System.Collections.Generic;

namespace XamlDilatation
{
    public class DomAttribute : IDisposable
    {
        public bool IsDisposed { get; set; }
        
        public readonly List<Type> UsedTypes; 
        
        public readonly XamlService Service; 
        
        public DomElement Parent { get; set; }
        
        public string Name { get; set; }
        
        public string Content { get; set; }
        
        public object MappedObject { get; set; }

        private Type _mappedObjectType;
        public Type MappedObjectType => _mappedObjectType ??= MappedObject.GetType();

        public DomAttribute(DomElement parent, string name, object obj, string content, XamlService service = null)
        {
            if (parent is null && service is null) throw new NullReferenceException();

            Name = name;
            MappedObject = obj;
            Parent = parent;
            Content = content;
            
            UsedTypes = parent is null ? new List<Type>() : parent.UsedTypes;
            Service = service ?? parent.Service;
        }
        
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            
            // Set everything null
            if (Parent is null) UsedTypes.Clear();
            Name = null;
            Parent = null;
            Content = null;
            MappedObject = null;
            _mappedObjectType = null;
        }
    }
}