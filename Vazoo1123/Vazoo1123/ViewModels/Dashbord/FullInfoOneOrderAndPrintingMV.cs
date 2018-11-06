using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Printing.Models;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Printing.ModalViews;

namespace Vazoo1123.ViewModels.Dashbord
{
    public class FullInfoOneOrderAndPrintingMV : BindableBase
    {
        private ManagerVazoo managerVazoo = null;
        private CAddressBase cAddressBase = null;
        private CAddressBase SourceAddr = null;
        private CDimensions cDimensions = null;
        private List<Carrier> carriers = null;

        public FullInfoOneOrderAndPrintingMV(OrderInfo orderInfo, ManagerVazoo managerVazoo)
        {
            carriers = new List<Carrier>();
            this.managerVazoo = managerVazoo;
            OrderInfo = orderInfo;
            IsValid = false;
            Carrier = orderInfo.CarrierOptimal;
            InitForFullInfoOrder(orderInfo.ShipToAddress);
        }

        private Carrier carrier = null;
        public Carrier Carrier
        {
            get { return carrier; }
            set { SetProperty(ref carrier, value); }
        }

        public OrderInfo orderInfo = null;
        public OrderInfo OrderInfo
        {
            get { return orderInfo; }
            set { SetProperty(ref orderInfo, value); }
        }

        public string name = "";
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string addres = "";
        public string Addres
        {
            get { return addres; }
            set { SetProperty(ref addres, value); }
        }

        public string city = "";
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        public string state = "";
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        public string zip = "";
        public string Zip
        {
            get { return zip; }
            set { SetProperty(ref zip, value); }
        }

        public string phone = "";
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        public string status = "";
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        public string comments = "";
        public string Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        private bool deliveryConfirmation = true;
        public bool DeliveryConfirmation
        {
            get { return deliveryConfirmation; }
            set { SetProperty(ref deliveryConfirmation, value); }
        }

        private bool signatureConfirmation = false;
        public bool SignatureConfirmation
        {
            get { return signatureConfirmation; }
            set { SetProperty(ref signatureConfirmation, value); }
        }

        private bool noValidate = false;
        public bool NoValidate
        {
            get { return noValidate; }
            set { SetProperty(ref noValidate, value); }
        }

        private bool signatureWaiver = true;
        public bool SignatureWaiver
        {
            get { return signatureWaiver; }
            set { SetProperty(ref signatureWaiver, value); }
        }

        private int labelsQty = 1;
        public int LabelsQty
        {
            get { return labelsQty; }
            set { SetProperty(ref labelsQty, value); }
        }

        public string TypeShipeMethod { get; set; }

        private List<Carrier> carriersUSPS = null;
        public List<Carrier> CarriersUSPS
        {
            get { return carriersUSPS; }
            set { SetProperty(ref carriersUSPS, value); }
        }

        private List<Carrier> carriersUPS = null;
        public List<Carrier> CarriersUPS
        {
            get { return carriersUPS; }
            set { SetProperty(ref carriersUPS, value); }
        }

        private List<Carrier> carriersFedEx = null;
        public List<Carrier> CarriersFedEx
        {
            get { return carriersFedEx; }
            set { SetProperty(ref carriersFedEx, value); }
        }


        private string strCalc = "";
        public string StrCalc
        {
            get => strCalc;
            set => SetProperty(ref strCalc, value);
        }

        public bool IsValid { get; set; } = false;

        private void InitForFullInfoOrder(CAddressBase cAddressBase)
        {
            Name = cAddressBase.Name;
            Addres = cAddressBase.Address2;
            City = cAddressBase.City;
            State = cAddressBase.State;
            Zip = cAddressBase.ZIP5;
            Phone = cAddressBase.Phone;
            Status = cAddressBase.Status;
            Comments = cAddressBase.Comments;
        }

        public async void DisplayShippingOptions()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = InitForDisplayShippingOptions();
            if (stateAuth == 3)
            {
                stateAuth = managerVazoo.PrintingWork("Options", ref description, cDimensions, SourceAddr, cAddressBase,
                    Convert.ToDouble(OrderInfo.WeightOZ != "" ? OrderInfo.WeightOZ.Replace(',', '.') : "0"), SignatureConfirmation, DeliveryConfirmation, NoValidate, 0, ref carriers, email, idCompany, psw);
            }
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                CarriersUSPS = new List<Carrier>(carriers.FindAll(c => c.Company == 1));
                CarriersUPS = new List<Carrier>(carriers.FindAll(c => c.Company == 2));
                CarriersFedEx = new List<Carrier>(carriers.FindAll(c => c.Company == 3));
                await PopupNavigation.PushAsync(new Views.PageApp.Dashbord.OptinsPage(this, CarriersUSPS.Count != 0, CarriersUPS.Count != 0, CarriersFedEx.Count != 0), true);
                IsValid = true;
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
                IsValid = false;
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
                IsValid = false;
            }  
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
                IsValid = false;
            }
        }

        private  int InitForDisplayShippingOptions()
        {
            cAddressBase = new CAddressBase();
            cDimensions = new CDimensions();
            SourceAddr = new CAddressBase();
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            if (stateAuth == 3)
            {
                cAddressBase.CompanyName = _xzType[2];
                cAddressBase.Name = _xzType[5];
                cAddressBase.Address1 = _xzType[10].Remove(_xzType[10].IndexOf(','));
                cAddressBase.Address2 = _xzType[10].Remove(0, _xzType[10].IndexOf(',') + 2);
                cAddressBase.City = _xzType[12];
                cAddressBase.State = _xzType[13];
                cAddressBase.ZIP5 = _xzType[14];
                cAddressBase.Phone = _xzType[15];
                cDimensions.Heigh = Convert.ToDouble(OrderInfo.DimensionsH != "" ? OrderInfo.DimensionsH.Replace('.', ',') : "0");
                cDimensions.Width = Convert.ToDouble(OrderInfo.DimensionsW != "" ? OrderInfo.DimensionsW.Replace('.', ',') : "0");
                cDimensions.Length = Convert.ToDouble(OrderInfo.DimensionsL != "" ? OrderInfo.DimensionsL.Replace('.', ',') : "0");
                if (orderInfo.ShipToAddress.Address1 != "")
                {
                    SourceAddr.Address1 = orderInfo.ShipToAddress.Address1;
                    SourceAddr.Address2 = orderInfo.ShipToAddress.Address2;
                    SourceAddr.Address3 = orderInfo.ShipToAddress.Address3;
                }
                else if (orderInfo.ShipToAddress.Address2 != "")
                {
                    SourceAddr.Address1 = orderInfo.ShipToAddress.Address2;
                    SourceAddr.Address2 = orderInfo.ShipToAddress.Address1;
                    SourceAddr.Address3 = orderInfo.ShipToAddress.Address3;
                }
                else
                {
                    SourceAddr.Address1 = orderInfo.ShipToAddress.Address3;
                    SourceAddr.Address2 = orderInfo.ShipToAddress.Address2;
                    SourceAddr.Address3 = orderInfo.ShipToAddress.Address1;
                }
                SourceAddr.City = orderInfo.ShipToAddress.City;
                SourceAddr.Comments = orderInfo.ShipToAddress.Comments;
                SourceAddr.CompanyName = orderInfo.ShipToAddress.CompanyName;
                SourceAddr.Name = orderInfo.ShipToAddress.Name;
                SourceAddr.Phone = orderInfo.ShipToAddress.Phone;
                SourceAddr.RDI = orderInfo.ShipToAddress.RDI;
                SourceAddr.State = orderInfo.ShipToAddress.State;
                SourceAddr.Status = orderInfo.ShipToAddress.Status;
                if (orderInfo.ShipToAddress.ZIP5.IndexOf('-') != -1)
                {
                    SourceAddr.ZIP5 = orderInfo.ShipToAddress.ZIP5.Remove(orderInfo.ShipToAddress.ZIP5.IndexOf('-'));
                }
                else
                {
                    SourceAddr.ZIP5 = orderInfo.ShipToAddress.ZIP5;
                }
            }
            return stateAuth;
        }

        public async void ShippingCreate()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string tracking = null; ;
            string shipingMethod = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string printerId = CrossSettings.Current.GetValueOrDefault("printerId", "");
            if (TypeShipeMethod == "USPS")
            {
                shipingMethod = "USPS_" + carrier.AccountID + "=" + carrier.Code + "=" + (carrier.IsL5 ? "Y" : carrier.IsShippo ? "S" : "N")
                   + "=" + carrier.RateID + "=" + carrier.Cost.ToString("f") + "=" + carrier.Price.ToString("f");
            }
            else if (TypeShipeMethod == "UPS")
            {
                shipingMethod = "UPS_" + carrier.Code;
            }
            else if (TypeShipeMethod == "FedEx")
            {
                shipingMethod = "FedEx_" + carrier.Code;
            }
            int stateAuth = managerVazoo.ShippingCreate(Convert.ToInt32(idCompany), email, psw, LabelsQty, shipingMethod, OrderInfo.ShopperEmail, SignatureWaiver, 
                Convert.ToDouble(OrderInfo.WeightOZ != "" ? OrderInfo.WeightOZ.Replace(',', '.') : "0"), cDimensions, SourceAddr,
                cAddressBase, DeliveryConfirmation, SignatureWaiver, NoValidate, true, "", "", "128", 0, ref tracking, ref description);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new LabalPageView(tracking), true);
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
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
    }
}