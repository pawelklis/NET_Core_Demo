using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IContactType
    {
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string AlternativeEmail { get; set; }
        public string WebSite { get; set; }
        public List<IWebLinkType> SocialMediaLinks { get; set; }
    }
}
