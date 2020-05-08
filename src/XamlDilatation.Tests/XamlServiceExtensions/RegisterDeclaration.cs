using System;
using System.Linq;
using Xunit;
// ReSharper disable InconsistentNaming

namespace XamlDilatation.Tests.XamlServiceExtensions
{
    /// <summary>
    /// Class for testing the RegisterDeclaration Region
    /// </summary>
    public class RegisterDeclaration
    {
        #region RegisterNamespace
        
        [Fact]
        public void RegisterNamespace_PrefixAndNamespaceFoundWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test0", "test.one", true);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_PrefixAndNamespaceFoundWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test0", "test.one", false);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_OnlyNamespaceWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test2", "test.one", true);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test2"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test2" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_OnlyNamespaceWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test2", "test.one", false);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_OnlyPrefixWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test1", "test.two", true);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.two" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_OnlyPrefixWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test1", "test.two", false);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
        }
        
        [Fact]
        public void RegisterNamespace_NothingFound()
        {
            var xamlService = new XamlService();
            xamlService.RegisterNamespace("test0", "test.zero");
            xamlService.RegisterNamespace("test1", "test.one");
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);

            xamlService.RegisterNamespace("test2", "test.two", true);
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 3);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test2"));
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition);
            Assert.Contains(xamlService.XmlnsDeclarations, o => o.Value.Declaration == "test.two" && o.Value.Prefix == "test2" && !o.Value.IsUrlDefinition);
        }
        
        #endregion
        
        #region RegisterUrl
        
        [Fact]
        public void RegisterUrl_PrefixAndUrlFoundWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", true, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", true, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test0", "http://zero", true, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.two", element0.Namespaces);
            Assert.Contains("test.2", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        [Fact]
        public void RegisterUrl_PrefixAndUrlFoundWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", false, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test0", "http://zero", false, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 4);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            Assert.Contains("test.two", element0.Namespaces);
            Assert.Contains("test.2", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        
        [Fact]
        public void RegisterUrl_OnlyUrlFoundWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", true, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", true, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test2", "http://zero", true, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test2"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test2" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.two", element0.Namespaces);
            Assert.Contains("test.2", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        
        [Fact]
        public void RegisterUrl_OnlyUrlFoundWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", false, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test2", "http://zero", false, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        
        [Fact]
        public void RegisterUrl_OnlyPrefixFoundWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", true, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", true, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test0", "http://two", true, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://two" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.two", element0.Namespaces);
            Assert.Contains("test.2", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        
        [Fact]
        public void RegisterUrl_OnlyPrefixFoundWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", false, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test2", "http://zero", false, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        
        [Fact]
        public void RegisterUrl_NothingFound()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterUrl("test1", "http://one", false, "test.one", "test.1");
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test2", "http://two", false, "test.two", "test.2");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 3);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test2"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            var element2 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://two" && o.Value.Prefix == "test2" && o.Value.IsUrlDefinition).Value;
            Assert.True(element2 != null);
            Assert.True(element2.Namespaces.Count == 2);
            Assert.Contains("test.two", element2.Namespaces);
            Assert.Contains("test.2", element2.Namespaces);
            
            #endregion TestAfter
        }
        
        #endregion
        
        #region RegisterUrlAndNamespace

        [Fact]
        public void RegisterUrlAndNamespace_UrlWithPrefixFromNsWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", true, "test.zero", "test.0");
            xamlService.RegisterNamespace("test1", "test.one", true);
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test1", "http://one", true, "test.one", "test.1");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://one" && o.Value.Prefix == "test1" && o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 2);
            Assert.Contains("test.one", element1.Namespaces);
            Assert.Contains("test.1", element1.Namespaces);
            
            #endregion TestAfter
        }
        
        [Fact]
        public void RegisterUrlAndNamespace_UrlWithPrefixFromNsWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterNamespace("test1", "test.one", false);
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestBefore
            
            xamlService.RegisterUrl("test1", "http://one", false, "test.one", "test.1");
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestAfter
        }
        
        [Fact]
        public void RegisterUrlAndNamespace_NsWithPrefixFromUrlWithOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", true, "test.zero", "test.0");
            xamlService.RegisterNamespace("test1", "test.one", true);
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestBefore
            
            xamlService.RegisterNamespace("test0", "test.zero", true);
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.zero" && o.Value.Prefix == "test0" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 1);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestAfter
        }
        
        [Fact]
        public void RegisterUrlAndNamespace_NsWithPrefixFromUrlWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterUrl("test0", "http://zero", false, "test.zero", "test.0");
            xamlService.RegisterNamespace("test1", "test.one", false);
            
            #region TestBefore
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            var element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            var element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestBefore
            
            xamlService.RegisterNamespace("test0", "test.zero", false);
            
            #region TestAfter
            
            Assert.True(xamlService.XmlnsDeclarations.Count == 2);
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test0"));
            Assert.True(xamlService.XmlnsDeclarations.ContainsKey("test1"));
            
            element0 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "http://zero" && o.Value.Prefix == "test0" && o.Value.IsUrlDefinition).Value;
            Assert.True(element0 != null);
            Assert.True(element0.Namespaces.Count == 2);
            Assert.Contains("test.zero", element0.Namespaces);
            Assert.Contains("test.0", element0.Namespaces);
            
            element1 = xamlService.XmlnsDeclarations.FirstOrDefault(o => o.Value.Declaration == "test.one" && o.Value.Prefix == "test1" && !o.Value.IsUrlDefinition).Value;
            Assert.True(element1 != null);
            Assert.True(element1.Namespaces.Count == 1);
            
            #endregion TestAfter
        }
        
        #endregion
    }
}