using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class TypeOfDeliveryType
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public int DeliveryInMinutes { get; set; }
        public List<DayOfWeek> DeliveryInSpecifiedDays { get; set; }

        public TypeOfDeliveryType()
        {
            this.DeliveryInSpecifiedDays = new List<DayOfWeek>();
        }

    }
}
