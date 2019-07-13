using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using Newtonsoft.Json;

namespace SearchEngineForSocialNetworks
{
    public class JScript
    {
        private IJavaScriptExecutor javaScriptExecutor;

        public JScript(IWebDriver driver)
        {
            this.javaScriptExecutor = (IJavaScriptExecutor)driver;
        }

        public void AuthorizationFacebookCommand(string login, string password)
        {
            var command = GetCommandByResourceName("AuthorizationOnFacebook.js", new Dictionary<string, string>
            {
                { "login", login },
                { "password", password }
            });
            javaScriptExecutor.ExecuteScript(command);
        }

        public void SearchUsersInFacebookByEmail(string email)
        {
            var command = GetCommandByResourceName("SearchUsersInFacebookByEmail.js", new Dictionary<string, string>
            {
                { "query", email }
            });
            javaScriptExecutor.ExecuteScript(command);
        }

        public List<FacebookUser> ParseInformationAboutUser()
        {
            var facebookUsers = new List<FacebookUser>();
            var command = GetCommandByResourceName("ParseInformationAboutUser.js", null);
            javaScriptExecutor.ExecuteScript(command);
            var result = javaScriptExecutor.ExecuteScript("return JSON.stringify(window.informationAboutUsers);") as string;
            if (!string.IsNullOrEmpty(result))
            {
                var usersOfFacebook = JsonConvert.DeserializeObject<List<FacebookUser>>(result);
                if (usersOfFacebook != null)
                {
                    foreach (var user in usersOfFacebook)
                    {
                        facebookUsers.Add(user);
                    }
                }
            }
            return facebookUsers;
        }

        private string GetCommandByResourceName(string fileName, Dictionary<string, string> variables)
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
                        string line = string.Empty;
                        while ((line = reader.ReadLine()) != null)
                        {
                            int i = 0; int j = 0;
                            if ((i = line.IndexOf("{{")) != -1 && (j = line.IndexOf("}}")) != -1)
                            {
                                i += 2;
                                var varName = line.Substring(i, j - i);
                                if (variables.ContainsKey(varName))
                                {
                                    line = line.Replace("{{" + varName + "}}", variables[varName]);
                                }
                                else
                                {
                                    throw new ArgumentException("Variables do not match the code");
                                }
                                sb.AppendLine(line);
                            }
                            else
                            {
                                sb.AppendLine(line);
                            }
                        }
                    }
                }
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}
