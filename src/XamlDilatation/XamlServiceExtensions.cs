using System;
using System.Linq;
using System.Reflection;
using System.Threading;
// ReSharper disable MemberCanBePrivate.Global

namespace XamlDilatation
{
    public static class XamlServiceExtensions
    {
        #region Register ContentProperty

        public static XamlService RegisterContentProperty(this XamlService service, Type type, string propertyName, bool isContentPropertyFlag)
        {
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            var propertyKey = PropertyKey.Get(propertyInfo);
            
            var setting = new ContentPropertySetting(propertyKey, isContentPropertyFlag);
            if (service.ContentPropertySettings.ContainsKey(propertyKey))
                service.ContentPropertySettings[propertyKey] = setting;
            else
                service.ContentPropertySettings.Add(propertyKey, setting);

            return service;
        }

        public static XamlService RegisterContentProperty(this XamlService service, Type type, string propertyName, Func<object, object, bool> isContentProperty)
        {
            if (isContentProperty is null) return service;
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            var propertyKey = PropertyKey.Get(propertyInfo);
            
            var setting = new ContentPropertySetting(propertyKey, isContentProperty);
            if (service.ContentPropertySettings.ContainsKey(propertyKey))
                service.ContentPropertySettings[propertyKey] = setting;
            else
                service.ContentPropertySettings.Add(propertyKey, setting);

            return service;
        }

        public static XamlService RegisterContentProperty<T>(this XamlService service, string propertyName, Func<T, object, bool> isContentProperty) =>
            RegisterContentProperty(service, typeof(T), propertyName, (obj, value) => isContentProperty.Invoke((T) obj, value));
        
        public static XamlService RegisterContentProperty<T>(this XamlService service, string propertyName, bool isContentPropertyFlag) =>
            RegisterContentProperty(service, typeof(T), propertyName, isContentPropertyFlag);
        
        public static ContentPropertySetting GetContentPropertySetting(this XamlService service, PropertyKey propertyKey)
        {
            if (service is null) return null;

            return service.ContentPropertySettings.ContainsKey(propertyKey) ? service.ContentPropertySettings[propertyKey] : null;
        }

        #endregion
        
        #region Register ChildrenProperty

        public static XamlService RegisterChildrenProperty(this XamlService service, Type type, string propertyName, bool isChildrenPropertyFlag)
        {
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            
            var setting = new ChildrenPropertySetting(propertyInfo, isChildrenPropertyFlag);
            if (service.ChildrenPropertySettings.ContainsKey(propertyInfo))
                service.ChildrenPropertySettings[propertyInfo] = setting;
            else
                service.ChildrenPropertySettings.Add(propertyInfo, setting);

            return service;
        }

        public static XamlService RegisterChildrenProperty(this XamlService service, Type type, string propertyName, IsChildrenPropertyDelegate isChildrenProperty)
        {
            if (isChildrenProperty is null) return service;
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            
            var setting = new ChildrenPropertySetting(propertyInfo, isChildrenProperty);
            if (service.ChildrenPropertySettings.ContainsKey(propertyInfo))
                service.ChildrenPropertySettings[propertyInfo] = setting;
            else
                service.ChildrenPropertySettings.Add(propertyInfo, setting);

            return service;
        }

        public static XamlService RegisterChildrenProperty<T>(this XamlService service, string propertyName, IsChildrenPropertyDelegate isChildrenProperty) =>
            RegisterChildrenProperty(service, typeof(T), propertyName, isChildrenProperty);
        
        public static XamlService RegisterChildrenProperty<T>(this XamlService service, string propertyName, bool isChildrenPropertyFlag) =>
            RegisterChildrenProperty(service, typeof(T), propertyName, isChildrenPropertyFlag);
        
        public static ChildrenPropertySetting GetChildrenPropertySetting(this XamlService service, PropertyInfo propertyInfo)
        {
            if (service is null) return null;

            return service.ChildrenPropertySettings.ContainsKey(propertyInfo) ? service.ChildrenPropertySettings[propertyInfo] : null;
        }


        #endregion
        
        #region Register StringSerializer

        public static XamlService RegisterStringSerializer<T>(this XamlService service, Func<T, string> serialize, Func<T, bool> shouldSerialize = null) =>
            RegisterStringSerializer(service, typeof(T),
                (obj, parentObj) => serialize((T) obj),
                (obj, parentObj) => shouldSerialize?.Invoke((T) obj) ?? true);
        
        public static XamlService RegisterStringSerializer<T, TParent>(this XamlService service, Func<T, TParent, string> serialize, Func<T, TParent, bool> shouldSerialize = null) =>
            RegisterStringSerializer(service, typeof(T),
                (obj, parentObj) => serialize((T) obj, (TParent) parentObj),
                (obj, parentObj) => shouldSerialize?.Invoke((T) obj, (TParent) parentObj) ?? true);

        public static XamlService RegisterStringSerializer(this XamlService service, Type type, Func<object, object, string> serialize, Func<object, object, bool> shouldSerialize = null)
        {
            if (serialize is null) return service;
            if (service is null) return null;

            var setting = new StringSerializerSetting(type, shouldSerialize, serialize);
            if (service.StringSerializerSettings.ContainsKey(type))
                service.StringSerializerSettings[type] = setting;
            else
                service.StringSerializerSettings.Add(type, setting);

            return service;
        }

        public static StringSerializerSetting GetSerializerSetting(this XamlService service, Type type)
        {
            if (service is null) return null;

            return service.StringSerializerSettings.ContainsKey(type) ? service.StringSerializerSettings[type] : null;
        }

        #endregion
        
        #region Register ObjectConverter
        
        public static XamlService RegisterObjectConverter<T, TParent, TOut>(this XamlService service, Func<T, TParent, TOut> convert, Func<T, TParent, bool> shouldConvert)
        {
            return service;
        }

        #endregion
        
        #region Register ShouldSerialize
        
        public static XamlService RegisterShouldSerialize(this XamlService service, Type type, string propertyName, bool shouldSerializeFlag)
        {
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            
            var setting = new ShouldSerializeSetting(propertyInfo, shouldSerializeFlag);
            if (service.ShouldSerializeSettings.ContainsKey(propertyInfo))
                service.ShouldSerializeSettings[propertyInfo] = setting;
            else
                service.ShouldSerializeSettings.Add(propertyInfo, setting);

            return service;
        }

        public static XamlService RegisterShouldSerialize(this XamlService service, Type type, string propertyName, Func<object, object, bool> shouldSerialize)
        {
            if (shouldSerialize is null) return service;
            if (service is null) return null;

            var propertyInfo = type.GetPublicPropertyInfo(propertyName);
            
            var setting = new ShouldSerializeSetting(propertyInfo, shouldSerialize);
            if (service.ShouldSerializeSettings.ContainsKey(propertyInfo))
                service.ShouldSerializeSettings[propertyInfo] = setting;
            else
                service.ShouldSerializeSettings.Add(propertyInfo, setting);

            return service;
        }

        public static XamlService RegisterShouldSerialize<T>(this XamlService service, string propertyName, Func<T, object, bool> shouldSerialize) =>
            RegisterShouldSerialize(service, typeof(T), propertyName,
                (obj, value) => shouldSerialize.Invoke((T) obj, value));
        
        public static XamlService RegisterShouldSerialize<T>(this XamlService service, string propertyName, bool shouldSerialize) =>
            RegisterShouldSerialize(service, typeof(T), propertyName, shouldSerialize);
        
        public static ShouldSerializeSetting GetShouldSerializeSetting(this XamlService service, PropertyInfo propertyInfo)
        {
            if (service is null) return null;

            return service.ShouldSerializeSettings.ContainsKey(propertyInfo) ? service.ShouldSerializeSettings[propertyInfo] : null;
        }
        
        #endregion
        
        #region Register Declaration

        /// <summary>
        /// Registers the namespace of the given Type with generated prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="overwrite">The flag if the existing should be overwritten</param>
        /// <typeparam name="T">The Type of which the namespace should get registered</typeparam>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace<T>(this XamlService service, bool overwrite = false) => 
            service.RegisterNamespace(typeof(T).Namespace, overwrite);

        /// <summary>
        /// Registers the namespace of the given Type with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="ns">The namespace that should get registered</param>
        /// <param name="overwrite">The flag if the existing should be overwritten</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace(this XamlService service, string ns, bool overwrite = false) => 
            service.RegisterNamespace(service.GeneratePrefix(ns), ns, overwrite);

        /// <summary>
        /// Registers the namespace of the given Type with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <param name="overwrite">The flag if the existing should be overwritten</param>
        /// <typeparam name="T">The Type of which the namespace should get registered</typeparam>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace<T>(this XamlService service, string prefix, bool overwrite = false) =>
            service.RegisterNamespace(prefix, typeof(T).Namespace, overwrite);

        /// <summary>
        /// Registers the namespace of the given Type with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <param name="ns">The namespace that should get registered</param>
        /// <param name="overwrite">The flag if the existing should be overwritten</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace(this XamlService service, string prefix, string ns, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(ns)) return service;
            if (string.IsNullOrEmpty(prefix)) return service;
            if (service is null) return null;
            
            var prefixExists = service.XmlnsDeclarations.ContainsKey(prefix);
            var foundNamespace = service.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == ns).Value;
            var newDeclaration = new XmlnsDeclaration(prefix, ns, false);

            // if both (namespace and prefix) exists -> abort
            if (prefixExists && foundNamespace != null) return service;
            // if only namespace exists and overwrite is false
            if (!overwrite && foundNamespace != null) return service;
            // if only namespace exists
            if(foundNamespace != null)
            {
                // delete the found registered
                service.XmlnsDeclarations.Remove(foundNamespace.Prefix);
                service.XmlnsDeclarations.Add(prefix, newDeclaration);
                return service;
            }
            // if only prefix exists and overwrite is false
            if (!overwrite && prefixExists) return service;
            // if only prefix exists and overwrite is true
            if(overwrite && prefixExists) service.XmlnsDeclarations[prefix] = newDeclaration;
            // if nothing exists
            else service.XmlnsDeclarations.Add(prefix, newDeclaration);
            
            return service;
        }

        /// <summary>
        /// Registers the namespaces to the given url with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <param name="url">The url that should get registered</param>
        /// <param name="overwrite">The flag if the existing should be overwritten</param>
        /// <param name="ns">The namespaces that should get registered to the url</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterUrl(this XamlService service, string prefix, string url, bool overwrite, params string[] ns)
        {
            if (string.IsNullOrEmpty(prefix)) return service;
            if (string.IsNullOrEmpty(url)) return service;
            if (ns is null || ns.Length == 0) return service;
            if (ns.Any(string.IsNullOrEmpty)) return service;

            var existingUrl = service.XmlnsDeclarations.FirstOrDefault(o => o.Value.IsUrlDefinition && o.Value.Url == url).Value;
            var prefixExists = service.XmlnsDeclarations.ContainsKey(prefix);
            var newDeclaration = new XmlnsDeclaration(prefix, url, true);
            newDeclaration.RegisterNamespace(ns);
            
            // url and prefix exists and overwrite false -> only add namespaces
            if (existingUrl != null && prefixExists && existingUrl.Prefix == prefix && !overwrite)
            {
                existingUrl.RegisterNamespace(ns);
                return service;
            }
            if(existingUrl != null && prefixExists && existingUrl.Prefix != prefix) return service;

            // only url exists and overwrite is false
            if (existingUrl != null && !overwrite) return service;
            // only url exists and overwrite is true
            if (existingUrl != null)
            {
                service.XmlnsDeclarations.Remove(existingUrl.Prefix);
                service.XmlnsDeclarations.Add(newDeclaration.Prefix, newDeclaration);
                return service;
            }

            // only prefix exists and overwrite is false
            if (prefixExists && !overwrite) return service;
            // only prefix exists and overwrite is true
            if (prefixExists)
            {
                service.XmlnsDeclarations[prefix] = newDeclaration;
                return service;
            }
            
            // nothing exists
            service.XmlnsDeclarations.Add(newDeclaration.Prefix, newDeclaration);
            
            
            return service;
        }
        
        #endregion
        
        #region Helper Methods

        /// <summary>
        /// Generates a unique prefix with the given namespace for the given service
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="ns">The namespaces that should get registered to the url</param>
        /// <returns>The generated prefix</returns>
        // ReSharper disable once InconsistentNaming
        private static string GeneratePrefix(this XamlService service, string ns)
        {
            // check if namespace is null or empty
            if (string.IsNullOrEmpty(ns)) return null;
            // helper lists -> prefixes and declarations
            var declarations = service.XmlnsDeclarations.Values.Select(o => o.Declaration).ToList();
            var prefixes = service.XmlnsDeclarations.Keys.ToList();
            // if namespace is already used
            if (declarations.Contains(ns)) return null;

            var resultPrefix = string.Empty;
            var splitted = ns.Split('.');
            var exists = true;
            var iteration = 0;
            var longest = splitted.Select(o => o.Length).OrderByDescending(o => o).First();

            // try combinations of the words from the namespaces
            while (exists)
            {
                iteration++;
                resultPrefix = splitted.Aggregate(string.Empty, (current, split) => current + string.Concat(split.Take(iteration)));
                resultPrefix = resultPrefix.ToLower();
                
                exists = prefixes.Contains(resultPrefix) || iteration == longest;
            }
            
            // if prefix is not used -> return result
            if (!prefixes.Contains(resultPrefix))
            {
                return resultPrefix;
            }
            
            // add x to the end until it is unique :-p
            exists = true;
            while (exists)
            {
                resultPrefix += 'x';
                exists =  prefixes.Contains(resultPrefix);
            }
            return resultPrefix;
        }

        #endregion
    }
}