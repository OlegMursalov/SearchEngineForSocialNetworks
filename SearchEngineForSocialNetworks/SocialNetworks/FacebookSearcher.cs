using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            using (IWebDriver driver = new ChromeDriver())
            {
                foreach (var email in emails)
                {
                    var i = email.IndexOf("@");
                    var accountUri = $"{Url}/{email.Substring(0, i)}";
                    driver.Navigate().GoToUrl(accountUri);
                    var javaScriptExecutor = (IJavaScriptExecutor)driver;
                    var sb = new StringBuilder();
                    sb.AppendLine("var collection = document.getElementsByClassName('_2nlw _2nlv');");
                    sb.AppendLine("if (collection && collection.length > 0) {");
                    sb.AppendLine("    return collection[0].text;");
                    sb.AppendLine("} else {");
                    sb.AppendLine("    return null;");
                    sb.AppendLine("}");
                    var result = (string)javaScriptExecutor.ExecuteScript(sb.ToString());
                    if (!string.IsNullOrEmpty(result))
                    {
                        list.Add(new FacebookUser { Name = result, IsFound = true, EmailAddress = email, Uri = accountUri });
                    }
                    else
                    {
                        list.Add(new FacebookUser { IsFound = false, EmailAddress = email });
                    }
                }
            }
            return list;
        }
    }
}