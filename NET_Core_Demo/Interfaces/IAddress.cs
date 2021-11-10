using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public interface IAddress
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Local { get; set; }
        public string ZipCode { get; set; }
    }
}
