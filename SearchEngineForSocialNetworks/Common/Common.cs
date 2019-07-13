using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineForSocialNetworks
{
    public static class Common
    {
        public static string GetTextFromEmbededResource(string fileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(fileName));
                if (!string.IsNullOrEmpty(resourceName))
                {
                    var sb = new StringBuilder();
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        public static string RetrieveTextFromTag(string html)
        {
            try
            {
                var i = html.IndexOf(">");
                var j = html.LastIndexOf("<");
                return html.Substring(i + 1, html.Length - j);
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }
    }
}
