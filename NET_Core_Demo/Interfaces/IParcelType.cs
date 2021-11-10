using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IParcelType
    {

        public string GuidId { get; set; }
        public string Number { get; set; }
        public SenderType Sender { get; set; }
        public RecipientType Recipient { get; set; }
        public PunctualityType Punctuality { get; set; }
        public List<ParcelEventType> ParcelEvents { get; set; }


    }
}
