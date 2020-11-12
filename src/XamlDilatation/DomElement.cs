using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
// ReSharper disable SuggestBaseTypeForParameter

namespace XamlDilatation
{
    public class DomElement : IDisposable
    {
        public bool IsDisposed { get; set; }
        
        public readonly List<Type> UsedTypes; 
        
        public readonly XamlService Service; 
        
        public DomElement Parent { get; set; }
        
        public DomElement Content { get; set; }
        
        public List<DomElement> Children { get; set; }

        public List<DomAttribute> Attributes { get; set; }
        
        public object MappedObject { get; set; }

        private Type _mappedObjectType;
        public Type MappedObjectType => _mappedObjectType ??= MappedObject.GetType();

        public DomElement(DomElement parent, object obj, XamlService service = null)
        {
            if (parent is null && service is null) throw new NullReferenceException();
            
            MappedObject = obj;
            Parent = parent;
            
            UsedTypes = parent is null ? new List<Type>() : parent.UsedTypes;
            Service = service ?? parent.Service;

            Children = new List<DomElement>();
            Attributes = new List<DomAttribute>();
        }

        public void Rebuild() => CreateHierarchy();

        private void CreateHierarchy()
        {
            var allProps = MappedObjectType.GetPublicProperties(out var types);
            AddTypes(types);
            
            // remove props with shouldSerializeSetting
            ExecuteShouldSerialize(allProps);
            // execute the object
            ExecuteObjectConverter(allProps);
            // set the content
            SetContent(allProps);
            // generate attributes
            GenerateAttributes(allProps);
            // generate children
            GenerateChildren(allProps);
        }

        private void ExecuteObjectConverter(List<PropertyInfo> allProps)
        {
            // shouldSerialize after type change
            // set the content after type change
            // generate attributes after type change
            // generate children after type change
        }

        private void ExecuteShouldSerialize(List<PropertyInfo> allProps)
        {
            foreach (var info in allProps.ToList())
            {
                var value = info.GetValue(MappedObject);
                var setting = Service.GetShouldSerializeSetting(info.PropertyType);
                if (setting is null) continue;

                if (setting.ShouldSerializeFlag.HasValue && !setting.ShouldSerializeFlag.Value
                    || !setting.ShouldSerializeFlag.HasValue && !setting.ShouldSerialize(value, MappedObject))
                {
                    allProps.Remove(info);
                }
            }
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        private void AddTypes(List<Type> foundTypes)
        {
            if (UsedTypes is null) return;

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var foundType in foundTypes)
            {
                if(UsedTypes.Contains(foundType)) continue;
                UsedTypes.Add(foundType);
            }
        }

        private void SetContent(List<PropertyInfo> allProps)
        {
            // get contentproperty
        }
        
        private void GenerateChildren(List<PropertyInfo> allProps)
        {
            // get childrenproperty
        }

        private void GenerateAttributes(List<PropertyInfo> allProps)
        {
            foreach (var info in allProps.ToList())
            {
                var value = info.GetValue(MappedObject);
                var serializer = Service.GetSerializerSetting(info.PropertyType);
                var alreadyString = info.PropertyType == typeof(string);

                if (serializer is null && !alreadyString) continue;
                if (alreadyString && serializer != null && serializer.ShouldSerialize(value, MappedObject))
                    value = serializer.Serialize(value, MappedObject);
                if (!alreadyString && !serializer.ShouldSerialize(value, MappedObject)) continue;

                var stringValue = alreadyString ? (string)value : serializer.Serialize(value, MappedObject);
                
                var attribute = new DomAttribute(this, info.Name, value, stringValue, Service);
                Attributes.Add(attribute);
                allProps.Remove(info);
            }
        }
        
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            
            // Dispose all disposable
            Content?.Dispose();
            Children?.ForEach(o => o?.Dispose());
            Attributes?.ForEach(o => o?.Dispose());
            
            // Set everything null
            if (Parent is null) UsedTypes.Clear();
            Parent = null;
            Content = null;
            Children?.Clear();
            Children = null;
            Attributes?.Clear();
            Attributes = null;
            MappedObject = null;
            _mappedObjectType = null;
        }
    }
}