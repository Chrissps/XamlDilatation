using System;
using System.Linq;
using Xunit;
// ReSharper disable InconsistentNaming

namespace XamlDilatation.Tests.XamlServiceExtensions
{
    /// <summary>
    /// Class for testing the RegisterDeclaration Region
    /// </summary>
    public class RegisterShouldSerialize
    {
        #region RegisterNamespace
        
        [Fact]
        public void RegisterShouldSerialize_OnlyTWithoutOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterShouldSerialize((int a) => true);
            
            #region Before
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.ContainsKey(typeof(int)));
            Assert.True(xamlService.ShouldSerializeSettings[typeof(int)].ShouldSerialize(2));
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((long a) => false);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 2);
            Assert.True(xamlService.ShouldSerializeSettings.ContainsKey(typeof(int)));
            Assert.True(xamlService.ShouldSerializeSettings.ContainsKey(typeof(long)));
            Assert.True(xamlService.ShouldSerializeSettings[typeof(int)].ShouldSerialize(2));
            Assert.True(!xamlService.ShouldSerializeSettings[typeof(long)].ShouldSerialize((long)2));
            
            #endregion After
        }
        
        [Fact]
        public void RegisterShouldSerialize_OnlyTOverwrite()
        {
            var xamlService = new XamlService();
            xamlService.RegisterShouldSerialize((int a) => true);
            
            #region Before
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.ContainsKey(typeof(int)));
            Assert.True(xamlService.ShouldSerializeSettings[typeof(int)].ShouldSerialize(2));
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((int a) => false);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.ContainsKey(typeof(int)));
            Assert.True(!xamlService.ShouldSerializeSettings[typeof(int)].ShouldSerialize(2));
            
            #endregion After
        }
        
        [Fact]
        public void RegisterShouldSerialize_AddTAddTParentBothExist()
        {
            var xamlService = new XamlService();
            xamlService.RegisterShouldSerialize((int a) => true);
            xamlService.RegisterShouldSerialize((int a, string text) => false);
            
            #region Before

            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out var val));
            Assert.True(val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(!val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((int a, string text) => false);
            xamlService.RegisterShouldSerialize((int a) => true);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out val));
            Assert.True(val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(!val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion After
        }
        
        [Fact]
        public void RegisterShouldSerialize_AddTAddTParentOnlyTExist()
        {
            var xamlService = new XamlService();
            xamlService.RegisterShouldSerialize((int a) => true);
            
            #region Before

            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out var val));
            Assert.True(val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 0);
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((int a, string text) => false);
            xamlService.RegisterShouldSerialize((int a) => false);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out val));
            Assert.True(!val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(!val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion After
        }
        
        [Fact]
        public void RegisterShouldSerialize_AddTAddTParentOnlyTParentExist()
        {
            var xamlService = new XamlService();
            xamlService.RegisterShouldSerialize((int a, string text) => true);
            
            #region Before

            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out var val));
            Assert.True(val.ShouldSerialize is null);
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((int a, string text) => false);
            xamlService.RegisterShouldSerialize((int a) => false);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out val));
            Assert.True(!val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(!val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion After
        }

        [Fact]
        public void RegisterShouldSerialize_AddTAddTParentNothingExists()
        {
            var xamlService = new XamlService();
            
            #region Before

            Assert.True(xamlService.ShouldSerializeSettings.Count == 0);
            Assert.True(!xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out var val));
            
            #endregion Before
            
            xamlService.RegisterShouldSerialize((int a, string text) => true);
            xamlService.RegisterShouldSerialize((int a) => true);
            
            #region After
            
            Assert.True(xamlService.ShouldSerializeSettings.Count == 1);
            Assert.True(xamlService.ShouldSerializeSettings.TryGetValue(typeof(int), out val));
            Assert.True(val.ShouldSerialize(2));
            Assert.True(val.ParentTypes.Count == 1);
            Assert.True(val.ParentTypes[typeof(string)](2, "test"));
            
            #endregion After
        }
        
        #endregion
    }
}