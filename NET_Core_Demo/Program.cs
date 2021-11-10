using System;

namespace NET_Core_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeOfDeliveryType dv = new TypeOfDeliveryType();
            dv.Code = "P";
            dv.Name = "Parcel standard";
            dv.DeliveryInMinutes = 24 * 60;
            dv.DeliveryInSpecifiedDays.Add(DayOfWeek.Monday);

            IOrganizationUnit org1 = new OrganizationUnitType();
            org1.Name = "Shipment Unit";
            org1.Code = "50900OR";

            IOrganizationUnit org2 = new OrganizationUnitType();
            org2.Name = "Transit Unit";
            org2.Code = "00900OR";

            IOrganizationUnit org3 = new OrganizationUnitType();
            org3.Name = "Deliver Unit";
            org3.Code = "90900OR";

            ParcelType.OnCreate += P_OnCreate;
            ParcelType p = new ParcelType(dv, new SenderType(), new RecipientType(), DateTime.Now, org1);
            
            p.OnShipment += P_OnShipment;
            p.OnDeliver += P_OnDeliver;

            var x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.IsShipment == true).Code, org1);
            x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.Code == "LV").Code, org1);
            x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.Code == "IN").Code, org2);
            x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.Code == "LV").Code, org2);
            x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.Code == "IN").Code, org3);
            x = p.AddEventAsync(ParcelEventType.GetParcelEvent().Find(x => x.IsDelivery == true).Code, org3);

            Console.ReadKey();
        }

        private static void P_OnDeliver(object sender, ParcelEventArgs e)
        {
            Console.WriteLine("Parcel Delivered");
            ParcelType p = (ParcelType)sender;
            Console.WriteLine("Parcel delivered?:" + p.Punctuality.IsServiceEnd + " parcel service time: " + p.Punctuality.ServiceTime().ToString());

            Console.WriteLine(  "History:");

            foreach(var v in p.ParcelEvents)
            {                
                Console.WriteLine("Time {0}, Organization unit: {1}, Event name: {2}", v.RegisteredTime.ToString(),v.OrganizationUnit.Name, v.Name  );
            }
        }

        private static void P_OnShipment(object sender, ParcelEventArgs e)
        {
            Console.WriteLine("Parcel Shipment");
        }

        private static void P_OnCreate(object sender, ParcelEventArgs e)
        {
            Console.WriteLine("Parcel Created " + e.Parcel.GuidId);
        }
    }
}
