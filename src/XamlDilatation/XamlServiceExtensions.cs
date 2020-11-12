using System;
using System.Linq;
using System.Threading;
// ReSharper disable MemberCanBePrivate.Global

namespace XamlDilatation
{
    public static class XamlServiceExtensions
    {
        #region Register ContentProperty

        public static XamlService RegisterContentProperty<T>(this XamlService service)
        {
            return service;
        }

        public static XamlService RegisterContentProperty<T, TParent>(this XamlService service)
        {
            return service;
        }

        public static XamlService RegisterContentProperty<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldUseFilter)
        {
            return service;
        }

        #endregion
        
        #region Register ChildrenProperty

        public static XamlService RegisterChildrenProperty<T>(this XamlService service)
        { 
            return service;
        }
        public static XamlService RegisterChildrenProperty<T, TParent>(this XamlService service)
        {
            return service;
        }
        
        public static XamlService RegisterChildrenProperty<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldUseFilter)
        {
            return service;
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
        
        public static XamlService RegisterShouldSerialize<T>(this XamlService service, bool shouldSerializeFlag)
        {
            if (service is null) return null;
            var type = typeof(T);
            
            var setting = new ShouldSerializerSetting(type, shouldSerializeFlag);
            if (service.ShouldSerializerSettings.ContainsKey(type))
                service.ShouldSerializerSettings[type] = setting;
            else
                service.ShouldSerializerSettings.Add(type, setting);

            return service;
        }

        public static XamlService RegisterShouldSerialize<T, TParent>(this XamlService service, bool shouldSerializeFlag)
        {
            if (service is null) return null;
            var type = typeof(T);
            var parentType = typeof(TParent);
            
            var setting = new ShouldSerializerSetting(type, parentType, shouldSerializeFlag);
            if (service.ShouldSerializerSettings.ContainsKey(type))
                service.ShouldSerializerSettings[type] = setting;
            else
                service.ShouldSerializerSettings.Add(type, setting);

            return service;
        }
        
        public static XamlService RegisterShouldSerializer<T>(this XamlService service, Func<T, bool> shouldSerialize) =>
            RegisterShouldSerializer(service, typeof(T),
                (obj, _) => shouldSerialize.Invoke((T) obj));
        
        public static XamlService RegisterShouldSerializer<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldSerialize) =>
            RegisterShouldSerializer(service, typeof(T),
                (obj, parentObj) => shouldSerialize((T) obj, (TParent) parentObj));

        public static XamlService RegisterShouldSerializer(this XamlService service, Type type, Func<object, object, bool> shouldSerialize = null)
        {
            if (shouldSerialize is null) return service;
            if (service is null) return null;

            var setting = new ShouldSerializerSetting(type, shouldSerialize);
            if (service.ShouldSerializerSettings.ContainsKey(type))
                service.ShouldSerializerSettings[type] = setting;
            else
                service.ShouldSerializerSettings.Add(type, setting);

            return service;
        }

        public static ShouldSerializerSetting GetShouldSerializeSetting(this XamlService service, Type type)
        {
            if (service is null) return null;

            return service.ShouldSerializerSettings.ContainsKey(type) ? service.ShouldSerializerSettings[type] : null;
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