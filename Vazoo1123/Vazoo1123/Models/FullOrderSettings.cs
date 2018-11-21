using System;
using System.Collections.Generic;
using Vazoo1123.ViewModels.Printing.Models;
using System.Linq;
using Prism.Mvvm;

namespace Vazoo1123.Models
{
    public class FullOrderSettings : BindableBase
    {
        public int ID { get; set; }
        public string EBayUserID { get; set; }
        public string ShopperEmail { get; set; }
        public string EBayItemID { get; set; }
        public string ImageFile { get; set; }
        public string RecordNumber { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string ItemTitle { get; set; }
        public bool IsAutoPrint { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal TransactionPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string DateCreated { get; set; }
        public string WeightLBS { get; set; }
        public string WeightOZ { get; set; }
        public string WeightKG { get; set; }
        public string DimensionsL { get; set; }
        public string DimensionsH { get; set; }
        public string DimensionsW { get; set; }
        public string TrackingNumber { get; set; }
        public Carrier CarrierOptimal { get; set; }
        public CAddressBase ShipToAddress { get; set; }
        //////////////////////////////////////////////////
        private Carrier carrier = null;
        public Carrier Carrier
        {
            get { return carrier; }
            set { SetProperty(ref carrier, value); }
        }
        public CAddressBase cAddressBase = null;
        public CDimensions cDimensions = null;
        public List<Carrier> CarriersUSPS { get; set; }
        public List<Carrier> CarriersUPS { get; set; }
        public List<Carrier> CarriersFedEx { get; set; }
        public string strCalc = "";
        public string StrCalc
        {
            get { return strCalc; }
            set { SetProperty(ref strCalc, value); }
        }
        public int LabelsQty { get; set; } = 1;
        public string Name { get; set; }
        public string Addres { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public List<Carrier> carriers = null;
        public string TypeShipeMethod { get; set; }

        public FullOrderSettings(OrderInfo orderInfo)
        {
            cAddressBase = new CAddressBase();
            cDimensions = new CDimensions();
            CarriersUSPS = new List<Carrier>();
            CarriersUPS = new List<Carrier>();
            CarriersFedEx = new List<Carrier>();
            carriers = new List<Carrier>();
            InitForFullInfoOrder(orderInfo);
        }

        private void InitForFullInfoOrder(OrderInfo orderInfo)
        {
            Name = orderInfo.ShipToAddress.Name;
            Addres = orderInfo.ShipToAddress.Address2;
            City = orderInfo.ShipToAddress.City;
            State = orderInfo.ShipToAddress.State;
            Zip = orderInfo.ShipToAddress.ZIP5;
            Phone = orderInfo.ShipToAddress.Phone;
            Status = orderInfo.ShipToAddress.Status;
            Comments = orderInfo.ShipToAddress.Comments;
            //////////////////////////////////////////////////////////
            cDimensions.Heigh = Convert.ToDouble(orderInfo.DimensionsH != "" ? orderInfo.DimensionsH.Replace('.', ',') : "0");
            cDimensions.Width = Convert.ToDouble(orderInfo.DimensionsW != "" ? orderInfo.DimensionsW.Replace('.', ',') : "0");
            cDimensions.Length = Convert.ToDouble(orderInfo.DimensionsL != "" ? orderInfo.DimensionsL.Replace('.', ',') : "0");
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            cAddressBase.Address1 = orderInfo.ShipToAddress.Address1;
            cAddressBase.Address2 = orderInfo.ShipToAddress.Address2;
            cAddressBase.Address3 = orderInfo.ShipToAddress.Address3;
            cAddressBase.City = orderInfo.ShipToAddress.City;
            cAddressBase.Comments = orderInfo.ShipToAddress.Comments;
            cAddressBase.CompanyName = orderInfo.ShipToAddress.CompanyName;
            cAddressBase.Name = orderInfo.ShipToAddress.Name;
            cAddressBase.Phone = orderInfo.ShipToAddress.Phone;
            cAddressBase.RDI = orderInfo.ShipToAddress.RDI;
            cAddressBase.State = orderInfo.ShipToAddress.State;
            cAddressBase.Status = orderInfo.ShipToAddress.Status;
            if (orderInfo.ShipToAddress.ZIP5.IndexOf('-') != -1)
            {
                cAddressBase.ZIP5 = orderInfo.ShipToAddress.ZIP5.Remove(orderInfo.ShipToAddress.ZIP5.IndexOf('-'));
            }
            else
            {
                cAddressBase.ZIP5 = orderInfo.ShipToAddress.ZIP5;
            }
        }

        public bool SetCarrier(string id)
        {
            bool isSelect = false;
            Carrier carrier1 = carriers.FirstOrDefault(c => c.Id.ToString() == id);
            if (carrier1 != null)
            {
                isSelect = true;
                Carrier = carrier1;
            }
            return isSelect;
        }

        public void SetCarrier(Carrier carrier1)
        {
            Carrier = carrier1;
            if(Carrier != null)
            {
                TypeShipeMethod = Carrier.CompanyName;
            }
        }
    }
}