using System.Collections.Generic;
using System.Linq;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace XamlDilatation
{
    public class XmlnsDeclaration
    {
        // todo also use assembly
        public string Prefix { get; }
        
        public List<string> Namespaces { get; } = new List<string>();
        
        public string Url { get; }

        public bool IsUrlDefinition => !string.IsNullOrEmpty(Url);

        public string Declaration => IsUrlDefinition ? Url : Namespaces.First();

        public XmlnsDeclaration(string prefix, string declaration, bool isUrl)
        {
            Prefix = prefix;
            if (isUrl) Url = declaration;
            else Namespaces.Add(declaration);
        }

        public void RegisterNamespace(params string[] ns)
        {
            foreach (var entry in ns)
            {
                if(Namespaces.Contains(entry)) continue;
                Namespaces.Add(entry);
            }
        }
    }
}