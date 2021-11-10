using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class SenderType : IPersonType
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public IAddress Address { get; set; }
        public IContactType Contact { get; set; }

    }
}
