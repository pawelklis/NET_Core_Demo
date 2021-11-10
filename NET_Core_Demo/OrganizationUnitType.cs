using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class OrganizationUnitType : IOrganizationUnit
    {
        string IOrganizationUnit.Name { get ; set ; }
        string IOrganizationUnit.Code { get ; set ; }
        string IAddress.City { get ; set ; }
        string IAddress.Street { get ; set ; }
        string IAddress.HouseNumber { get ; set ; }
        string IAddress.Local { get ; set ; }
        string IAddress.ZipCode { get ; set ; }
    }
}
