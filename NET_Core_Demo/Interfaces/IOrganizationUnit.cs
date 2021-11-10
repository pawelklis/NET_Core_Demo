using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IOrganizationUnit:IAddress
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
