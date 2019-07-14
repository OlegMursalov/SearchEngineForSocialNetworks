using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineForSocialNetworks
{
    public class TwitterSearcher : UniversalSearcher
    {
        protected override string Url
        {
            get
            {
                return "https://twitter.com";
            }
        }

        public TwitterSearcher(IEnumerable<string> emails) : base(emails)
        {
        }

        public List<TwitterUser> Process()
        {
            var list = new List<TwitterUser>();
            var responses = base.GetResponsesByEmails();
            foreach (var response in responses)
            {
                if (response.Document != null)
                {
                    var document = response.Document;
                    var accountUri = response.AccountUri;
                    var email = response.Email;
                    var aTags = document.DocumentNode.Descendants("a");
                    if (aTags != null && aTags.Count() > 0)
                    {
                        var aTag = aTags.FirstOrDefault(x => x.HasClass("ProfileHeaderCard-nameLink"));
                        if (aTag != null)
                        {
                            list.Add(new TwitterUser { IsFound = true, EmailAddress = response.Email, Name = aTag.InnerText, Uri = response.AccountUri });
                        }
                    }
                }
                else if (response.IsNotFound404)
                {
                    list.Add(new TwitterUser { IsFound = false, EmailAddress = response.Email });
                }
            }
            return list;
        }
    }
}