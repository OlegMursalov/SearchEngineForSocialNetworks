using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineForSocialNetworks
{
    public class InstagramSearcher
    {
        private IEnumerable<string> emails;
        private string url = "https://www.instagram.com";

        public InstagramSearcher(IEnumerable<string> emails)
        {
            this.emails = emails;
        }

        public List<InstagramUser> Process()
        {
            var list = new List<InstagramUser>();
            foreach (var email in emails)
            {
                var i = email.IndexOf("@");
                var accountUri = $"{url}/{email.Substring(0, i)}";
                try
                {
                    var request = WebRequest.Create(accountUri);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            var line = string.Empty;
                            using (var reader = new StreamReader(stream))
                            {
                                var html = reader.ReadToEnd();
                                var document = new HtmlDocument();
                                document.LoadHtml(html);
                                var scripts = document.DocumentNode.Descendants("script");
                                if (scripts != null && scripts.Count() > 0)
                                {
                                    foreach (var script in scripts)
                                    {
                                        if (script.HasAttributes)
                                        {
                                            foreach (var attr in script.Attributes)
                                            {
                                                if (attr.Name == "type" && attr.Value == "application/ld+json")
                                                {
                                                    var user = JsonConvert.DeserializeObject<InstagramUser>(script.InnerText);
                                                    user.IsFound = true;
                                                    user.Uri = accountUri;
                                                    user.EmailAddress = email;
                                                    list.Add(user);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                    {
                        var response = (HttpWebResponse)ex.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            // Not Found (404)
                            list.Add(new InstagramUser { IsFound = false, EmailAddress = email });
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return list;
        }
    }
}