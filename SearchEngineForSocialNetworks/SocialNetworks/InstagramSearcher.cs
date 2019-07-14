using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineForSocialNetworks
{
    public class InstagramSearcher : UniversalSearcher
    {
        protected override string Url
        {
            get
            {
                return "https://www.instagram.com";
            }
        }

        public InstagramSearcher(IEnumerable<string> emails) : base(emails)
        {
        }

        public List<InstagramUser> Process()
        {
            var list = new List<InstagramUser>();
            var responses = base.GetResponsesByEmails();
            foreach (var response in responses)
            {
                if (response.Document != null)
                {
                    var document = response.Document;
                    var accountUri = response.AccountUri;
                    var email = response.Email;
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
                else if (response.IsNotFound404)
                {
                    list.Add(new InstagramUser { IsFound = false, EmailAddress = response.Email });
                }
            }
            return list;
        }
    }
}