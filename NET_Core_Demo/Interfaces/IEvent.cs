using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IEvent
    {
        public string GuidId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsShipment { get; set; }
        public bool IsDelivery { get; set; }
        public DateTime RegisteredTime { get; set; }

        public IOrganizationUnit OrganizationUnit { get; set; }
    }
}
