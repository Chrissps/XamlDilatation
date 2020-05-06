using System.Collections.Generic;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace XamlDilatation
{
    public class XmlnsDeclaration
    {
        public string Prefix { get; }
        
        public List<string> Namespace { get; } = new List<string>();
        
        public string Url { get; }

        public bool IsUrlDefinition => !string.IsNullOrEmpty(Url);

        public XmlnsDeclaration(string prefix, string declaration, bool isUrl)
        {
            Prefix = prefix;
            if (isUrl) Url = declaration;
            else Namespace.Add(declaration);
        }

        public void RegisterNamespace(params string[] ns)
        {
            foreach (var entry in ns)
            {
                if(Namespace.Contains(entry)) continue;
                Namespace.Add(entry);
            }
        }
    }
}