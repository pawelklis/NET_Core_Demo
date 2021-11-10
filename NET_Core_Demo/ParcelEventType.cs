using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class ParcelEventType : IEvent
    {
        public string GuidId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsShipment { get; set; }
        public bool IsDelivery { get; set; }
        public DateTime RegisteredTime { get; set; }
        public IOrganizationUnit OrganizationUnit { get; set; }

        public ParcelEventType()
        {
            this.GuidId = Guid.NewGuid().ToString();
        }



        public static List<ParcelEventType> GetParcelEvent()
        {
            // this suppose to be get from DB, in demo just created objects
            List<ParcelEventType> list = new List<ParcelEventType>();

            list.Add(new ParcelEventType() { Code = "SH", IsShipment = true, Name="Shipment" });
            list.Add(new ParcelEventType() { Code = "DV", IsDelivery = true, Name = "Delivery" });
            list.Add(new ParcelEventType() { Code = "MD", Name = "Modification" });
            list.Add(new ParcelEventType() { Code = "CR", Name = "Created" });
            list.Add(new ParcelEventType() { Code = "IN", Name = "Arrived" });
            list.Add(new ParcelEventType() { Code = "LV", Name = "Leave" });

            return list;
        }

    }
}
