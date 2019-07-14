using HtmlAgilityPack;

namespace SearchEngineForSocialNetworks
{
    public class ResponseDTO
    {
        public HtmlDocument Document { get; set; }
        public bool IsNotFound404 { get; set; }
        public string Exception { get; set; }
        public string AccountUri { get; set; }
        public string Email { get; set; }
    }
}