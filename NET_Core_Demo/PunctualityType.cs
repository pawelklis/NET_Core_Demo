using System;
using System.Collections.Generic;
using System.Text;

namespace NET_Core_Demo
{
    public class PunctualityType
    {
        public event EventHandler OnShipmentChange;
        public event EventHandler OnDeliveryChange;

        private TypeOfDeliveryType deliverytypeField;
        public TypeOfDeliveryType DeliveryType
        {
            get { return deliverytypeField; }
            set { 
                this.deliverytypeField = value; 
                this.SetPredictedPunctuality();
            }
        }
        private DateTime realShipmentTimeField;
        public DateTime RealShipmentTime { get { return realShipmentTimeField; }  set {
                this.realShipmentTimeField = value;
                EventHandler handler = OnShipmentChange;
                handler?.Invoke(this, null);
            } }

        private DateTime realDeliveryTimeField;
        public DateTime RealDeliveryTime { get { return realDeliveryTimeField; } set {
                this.realDeliveryTimeField = value;
                this.IsServiceEnd = true;
                EventHandler handler = OnDeliveryChange;
                handler?.Invoke(this, null);
            } }

        private DateTime predictedShipmentTimeField;
        public DateTime PredictedShipmentTime
        {
            get { return predictedShipmentTimeField; }
            set
            {
                predictedShipmentTimeField = value;
                SetPredictedPunctuality();
            }
        }

        public DateTime PredictedDeliveryTime { get; private set; }
        public bool IsServiceEnd { get; private set; }


        private void SetPredictedPunctuality()
        {
            this.PredictedDeliveryTime = this.PredictedShipmentTime.AddMinutes(this.DeliveryType.DeliveryInMinutes);

            if (this.DeliveryType.DeliveryInSpecifiedDays.Count > 0)
            {
                CheckDayOfDelivery();
            }

        }

        private void CheckDayOfDelivery()
        {
            if (!this.DeliveryType.DeliveryInSpecifiedDays.Contains(this.PredictedDeliveryTime.DayOfWeek))
            {
                this.PredictedDeliveryTime = this.PredictedDeliveryTime.AddDays(1);
                CheckDayOfDelivery();
            }

        }

        public TimeSpan ServiceTime()
        {
            return RealDeliveryTime - RealShipmentTime;
        }
        public TimeSpan PredictedServiceTime()
        {
            return PredictedDeliveryTime - PredictedShipmentTime;
        }


        public PuntualityStatus IsOnTime()
        {
            if (IsServiceEnd == false)
                return PuntualityStatus.InService;

            if (PredictedServiceTime() <= ServiceTime())
                return PuntualityStatus.OnTime;
            else
                return PuntualityStatus.NotOnTime;
        }

        public enum PuntualityStatus
        {
            OnTime,
            NotOnTime,
            InService
        }
    }
}
