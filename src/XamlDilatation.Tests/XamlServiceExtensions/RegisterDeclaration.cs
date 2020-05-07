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
        
        
        #endregion
        
        #region RegisterUrlAndNamespace
        
        
        #endregion
    }
}