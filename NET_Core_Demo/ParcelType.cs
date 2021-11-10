using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NET_Core_Demo
{
    public class ParcelType : IParcelType
    {
        public string GuidId { get; set; }
        public string Number { get; set; }
        public SenderType Sender { get; set; }
        public RecipientType Recipient { get; set; }
        public PunctualityType Punctuality { get; set; }
        public List<ParcelEventType> ParcelEvents { get; set; }
        public static event EventHandler<ParcelEventArgs> OnCreate;
        public event EventHandler<ParcelEventArgs> OnShipment;
        public event EventHandler<ParcelEventArgs> OnDeliver;

        public ParcelType()
        {
            this.GuidId = Guid.NewGuid().ToString();
        }

        public ParcelType(TypeOfDeliveryType deliveryType, SenderType sender, RecipientType recipient, DateTime predictedShipmentTime, IOrganizationUnit org)
        {
            this.ParcelEvents = new List<ParcelEventType>();
            this.GuidId = Guid.NewGuid().ToString();
            this.Recipient = recipient;
            this.Sender = sender;
            this.Punctuality = new PunctualityType();
            this.Punctuality.DeliveryType = deliveryType;
            this.Punctuality.PredictedShipmentTime = predictedShipmentTime;

            this.Punctuality.OnShipmentChange += Punctuality_OnShipmentChange;
            this.Punctuality.OnDeliveryChange += Punctuality_OnDeliveryChange;



            CheckAutoEventsAsync(AutoEventsType.EventTypeTrigger.OnCreate, org);


            EventHandler<ParcelEventArgs> handler = OnCreate;
            handler?.Invoke(this, new ParcelEventArgs() { Parcel = this });

        }

        private void Punctuality_OnDeliveryChange(object sender, EventArgs e)
        {
            EventHandler<ParcelEventArgs> handler = OnDeliver;
            handler?.Invoke(this, new ParcelEventArgs() { Parcel = this });
        }

        private void Punctuality_OnShipmentChange(object sender, EventArgs e)
        {
            EventHandler<ParcelEventArgs> handler = OnShipment;
            handler?.Invoke(this, new ParcelEventArgs() { Parcel = this });
        }

        public async Task AddEventAsync(string EventCode, IOrganizationUnit org)
        {
            await Task.Run(() => {      
                AddEvent(EventCode, org);
            });
        }
        public void AddEvent(string EventCode, IOrganizationUnit org)
        {
            List<ParcelEventType> events = ParcelEventType.GetParcelEvent();
            events.ForEach(x =>
            {
                if (x.Code == EventCode)
                {
                    this.ParcelEvents.Add(new ParcelEventType()
                    {
                        GuidId = x.GuidId,
                        Code = x.Code,
                        IsDelivery = x.IsDelivery,
                        IsShipment = x.IsShipment,
                        Name = x.Name,
                        RegisteredTime = DateTime.Now,
                        OrganizationUnit=org
                    });
                    if (x.IsShipment == true)
                        this.Punctuality.RealShipmentTime = DateTime.Now;
                    if (x.IsDelivery == true)
                    {
                        this.Punctuality.RealDeliveryTime = DateTime.Now;                        
                    }
                }

                return;
            });
        }

        private void CheckAutoEventsAsync(AutoEventsType.EventTypeTrigger trigger, IOrganizationUnit org)
        {
            // Get from DB
            List<AutoEventsType> events = AutoEventsType.GetAutoEvents();

            Parallel.ForEach(events, async x =>
            {
                if (x.DeliveryCode == this.Punctuality.DeliveryType.Code && x.Trigger == trigger)
                    await AddEventAsync(x.EventCode, org);
            });

            //Other examples of loop:
            //events.ForEach(async x =>
            //{
            //    if (x.DeliveryCode == this.Punctuality.DeliveryType.Code && x.Trigger == trigger)
            //        await AddEventAsync(x.EventCode, org);
            //});

            //foreach (var x in events)
            //{
            //    if (x.DeliveryCode == this.Punctuality.DeliveryType.Code && x.Trigger == trigger)
            //        AddEventAsync(x.EventCode, org);
            //}

            //for (int i = 0; i < events.Count; i++)
            //{
            //    if (events[i].DeliveryCode == this.Punctuality.DeliveryType.Code && events[i].Trigger == trigger)
            //        _ = AddEventAsync(events[i].EventCode, org);
            //}

            //int ii = 0;
            //do
            //{
            //    if (events[ii].DeliveryCode == this.Punctuality.DeliveryType.Code && events[ii].Trigger == trigger)
            //        _ = AddEventAsync(events[ii].EventCode, org);
            //    ii++;

            //} while (ii < events.Count);


        }

    }
}
