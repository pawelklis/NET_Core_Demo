using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class AutoEventsType
    {

        public string EventGuidId { get; set; }
        public string DeliveryCode { get; set; }
        public string EventCode { get; set; }
        public EventTypeTrigger Trigger { get; set; }

        public static List<AutoEventsType> GetAutoEvents()
        {
            // this should be getted from DB or webservice/api etc...
            List<AutoEventsType> events = new List<AutoEventsType>();
            events.Add(new AutoEventsType() { DeliveryCode = "P", EventCode = "SH", Trigger = AutoEventsType.EventTypeTrigger.OnShipment });
            events.Add(new AutoEventsType() { DeliveryCode = "P", EventCode = "DV", Trigger = AutoEventsType.EventTypeTrigger.OnDeliver });
            events.Add(new AutoEventsType() { DeliveryCode = "P", EventCode = "CR", Trigger = AutoEventsType.EventTypeTrigger.OnCreate });
            return events;
        }

        public enum EventTypeTrigger
        {
            OnCreate,
            OnShipment,
            OnDeliver
        }
    }
}
