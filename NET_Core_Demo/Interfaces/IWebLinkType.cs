using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IWebLinkType
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
