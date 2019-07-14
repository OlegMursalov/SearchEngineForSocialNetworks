using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SearchEngineForSocialNetworks
{
    public class UniversalSearcher
    {
        protected IEnumerable<string> emails;
        protected virtual string Url { get; }

        public UniversalSearcher(IEnumerable<string> emails)
        {
            this.emails = emails;
        }

        public List<ResponseDTO> GetResponsesByEmails()
        {
            var responses = new List<ResponseDTO>();
            foreach (var email in emails)
            {
                var i = email.IndexOf("@");
                var accountUri = $"{Url}/{email.Substring(0, i)}";
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
                                responses.Add(new ResponseDTO { Document = document, IsNotFound404 = false, Exception = null, AccountUri = accountUri, Email = email });
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
                            responses.Add(new ResponseDTO { Document = null, IsNotFound404 = true, Exception = ex.Message, AccountUri = accountUri, Email = email });
                        }
                    }
                }
                catch (Exception ex)
                {
                    responses.Add(new ResponseDTO { Document = null, IsNotFound404 = true, Exception = ex.Message, AccountUri = accountUri, Email = email });
                }
            }
            return responses;
        }
    }
}