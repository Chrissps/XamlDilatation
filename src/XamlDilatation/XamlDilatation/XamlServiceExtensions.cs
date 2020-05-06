using System;
using System.Threading;
// ReSharper disable MemberCanBePrivate.Global

namespace XamlDilatation
{

    public static class XamlServiceExtensions
    {
        #region Register ContentProperty

        public static XamlService RegisterContentProperty<T>(this XamlService service, RegisterPriority priority) => service.RegisterContentProperty(typeof(T), priority);
        public static XamlService RegisterContentProperty<T, TParent>(this XamlService service, RegisterPriority priority) => service.RegisterContentProperty(typeof(T),typeof(TParent), priority);
        public static XamlService RegisterContentProperty(this XamlService service, Func<object, object, bool> shouldUseFilter, RegisterPriority priority) => service.RegisterContentProperty<object, object>(shouldUseFilter, priority);
        
        public static XamlService RegisterContentProperty(this XamlService service, Type property, RegisterPriority priority)
        {
            service.RegisterContentProperty<object>(RegisterPriority.Highest);
            return service;
        }

        public static XamlService RegisterContentProperty(this XamlService service, Type property, Type parent, RegisterPriority priority)
        {
            return service;
        }
        
        public static XamlService RegisterContentProperty<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldUseFilter, RegisterPriority priority)
        {
            return service;
        }

        #endregion
        
        #region Register ChildrenProperty

        public static XamlService RegisterChildrenProperty<T>(this XamlService service, RegisterPriority priority) => service.RegisterChildrenProperty(typeof(T), priority);
        public static XamlService RegisterChildrenProperty<T, TParent>(this XamlService service, RegisterPriority priority) => service.RegisterChildrenProperty(typeof(T),typeof(TParent), priority);
        public static XamlService RegisterChildrenProperty(this XamlService service, Func<object, object, bool> shouldUseFilter, RegisterPriority priority) => service.RegisterChildrenProperty<object, object>(shouldUseFilter, priority);
        
        public static XamlService RegisterChildrenProperty(this XamlService service, Type property, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterChildrenProperty(this XamlService service, Type property, Type parent, RegisterPriority priority)
        {
            return service;
        }
        
        public static XamlService RegisterChildrenProperty<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldUseFilter, RegisterPriority priority)
        {
            return service;
        }

        #endregion
        
        #region Register StringSerializer
        
        public static XamlService RegisterStringSerializer<T>(this XamlService service, Func<T, string> serialize, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterStringSerializer<T>(this XamlService service, Func<T, string> serialize, Func<T, bool> shouldSerialize, RegisterPriority priority)
        {
            return service;
        }
        
        public static XamlService RegisterStringSerializer<T, TParent>(this XamlService service, Func<T, TParent, string> serialize, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterStringSerializer<T, TParent>(this XamlService service, Func<T, TParent, string> serialize, Func<T, TParent, bool> shouldSerialize, RegisterPriority priority)
        {
            return service;
        }

        #endregion
        
        #region Register ObjectConverter
        
        public static XamlService RegisterObjectConverter<T, TOut>(this XamlService service, Func<T, TOut> convert, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterObjectConverter<T, TOut>(this XamlService service, Func<T, TOut> convert, Func<T, bool> shouldConvert, RegisterPriority priority)
        {
            return service;
        }
        
        public static XamlService RegisterObjectConverter<T, TParent, TOut>(this XamlService service, Func<T, TParent, TOut> convert, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterObjectConverter<T, TParent, TOut>(this XamlService service, Func<T, TParent, TOut> convert, Func<T, TParent, bool> shouldConvert, RegisterPriority priority)
        {
            return service;
        }

        #endregion
        
        #region Register ShouldSerialize
        
        public static XamlService RegisterShouldSerialize<T>(this XamlService service, Func<T, bool> shouldSerialize, RegisterPriority priority)
        {
            return service;
        }

        public static XamlService RegisterShouldSerialize<T, TParent>(this XamlService service, Func<T, TParent, bool> shouldSerialize, RegisterPriority priority)
        {
            return service;
        }

        #endregion

        #region Register ThreadExecution
        
        public static XamlService RegisterThreadExecution(this XamlService service, Thread thread, RegisterPriority priority)
        {
            return service;
        }
        
        public static XamlService RegisterThreadExecution(this XamlService service, Action<Action> executeInside, RegisterPriority priority)
        {
            return service;
        }
        
        #endregion
        
        #region Register Namespace
        
        /// <summary>
        /// Registers the namespace of the given Type with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <typeparam name="T">The Type of which the namespace should get registered</typeparam>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace<T>(this XamlService service, string prefix)
        {
            return service;
        }
        
        /// <summary>
        /// Registers the namespace of the given Type with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <param name="ns">The namespace that should get registered</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterNamespace(this XamlService service, string prefix, string ns)
        {
            if (service.XmlnsDeclarations.TryGetValue(prefix, out var xmlnsDeclaration))
            {
                
            }
            
            return service;
        }
        
        /// <summary>
        /// Registers the namespaces to the given url with generated prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="url">The url that should get registered</param>
        /// <param name="ns">The namespaces that should get registered to the url</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterUrl(this XamlService service, string url, params string[] ns)
        {
            return service;
        }
        
        /// <summary>
        /// Registers the namespaces to the given url with the given prefix
        /// </summary>
        /// <param name="service">The service to register to</param>
        /// <param name="prefix">The prefix that should be used</param>
        /// <param name="url">The url that should get registered</param>
        /// <param name="ns">The namespaces that should get registered to the url</param>
        /// <returns>The service itself</returns>
        public static XamlService RegisterUrl(this XamlService service, string prefix, string url, params string[] ns)
        {
            return service;
        }
        
        #endregion
    }
}