using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineForSocialNetworks
{
    public class FacebookSearcher : UniversalSearcher
    {
        protected override string Url
        {
            get
            {
                return "https://www.facebook.com";
            }
        }

        public FacebookSearcher(IEnumerable<string> emails) : base(emails)
        {
        }

        public List<FacebookUser> Process()
        {
            var list = new List<FacebookUser>();
            var responses = base.GetResponsesByEmails();
            foreach (var response in responses)
            {
                if (response.Document != null)
                {
                    var document = response.Document;
                    var accountUri = response.AccountUri;
                    var email = response.Email;
                    var h1Tags = document.DocumentNode.Descendants("h1");
                    if (h1Tags != null && h1Tags.Count() > 0)
                    {
                        var h1 = h1Tags.FirstOrDefault(x => x.HasClass("_2nlv"));
                        if (h1 != null)
                        {
                            list.Add(new FacebookUser { IsFound = true, EmailAddress = response.Email, Name = h1.InnerText, Uri = response.AccountUri });
                        }
                    }
                }
            }
            return list;
        }
    }
}