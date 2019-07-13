using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineForSocialNetworks
{
    public class InstagramUser
    {
        public string Name { get; set; }
        public string AlternateName { get; set; }
        public string Image { get; set; }

        public string Uri { get; set; }
        public string EmailAddress { get; set; }
        public bool IsFound { get; set; }
    }
}