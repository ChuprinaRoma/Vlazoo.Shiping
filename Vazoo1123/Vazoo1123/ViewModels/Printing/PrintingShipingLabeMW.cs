using Plugin.Settings;
using Prism.Commands;
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

namespace Vazoo1123.ViewModels.Printing
{
    public class PrintingShipingLabeMW : BindableBase , ICreateShiping
    {
        public DelegateCommand InitToAddressCommand { get; set; }
        public DelegateCommand InitFromAddressCommand { get; set; }
        public DelegateCommand DisplayShippingOptionsCommand { get; set; }
        private ManagerVazoo managerVazoo = null;
        private CAddressBase cAddressBase = null;
        private CAddressBase SourceAddr = null;
        private CDimensions cDimensions = null;
        private List<Carrier> carriers = null;

        public PrintingShipingLabeMW(ManagerVazoo managerVazoo)
        {
            carriers = new List<Carrier>();
            this.managerVazoo = managerVazoo;
            cAddressBase = new Vazoo1123.Models.CAddressBase();
            SourceAddr = new Vazoo1123.Models.CAddressBase();
            IsValid = false;
            cDimensions = new CDimensions();
            InitToAddressCommand = new DelegateCommand(InitToAddress);
            InitFromAddressCommand = new DelegateCommand(InitFromAddress);
            DisplayShippingOptionsCommand = new DelegateCommand(DisplayShippingOptions);
            Init();
        }

        private Carrier carrier = null;
        public Carrier Carrier
        {
            get { return carrier; }
            set { SetProperty(ref carrier, value); }
        }

        private string balance = "0";
        public string Balance
        {
            get { return balance; }
            set { SetProperty(ref balance, "$"+value); }
        }
        /////////////////////////////////////////////////////
        private string fromCompanyName = "";
        public string FromCompanyName
        {
            get { return fromCompanyName; }
            set
            {
                SetProperty(ref fromCompanyName, value);
                SourceAddr.CompanyName = fromCompanyName;
            }
        }

        private string fromContactName = "";
        public string FromContactName
        {
            get { return fromContactName; }
            set
            {
                SetProperty(ref fromContactName, value);
                SourceAddr.Name = fromContactName;
            }
        }

        private string fromAddress1 = "";
        public string FromAddress1
        {
            get { return fromAddress1; }
            set
            {
                SetProperty(ref fromAddress1, value);
                SourceAddr.Address1 = fromAddress1;
            }
        }

        private string fromAddress2 = "";
        public string FromAddress2
        {
            get { return fromAddress2; }
            set 
            {
                SetProperty(ref fromAddress2, value);
                SourceAddr.Address2 = fromAddress2;
            }
        }

        private string fromCity = "";
        public string FromCity
        {
            get { return fromCity; }
            set
            {
                SetProperty(ref fromCity, value);
                SourceAddr.City = fromCity;
            }
        }

        private string fromState = "";
        public string FromState
        {
            get { return fromState; }
            set
            {
                SetProperty(ref fromState, value);
                SourceAddr.State = fromState;
            }
        }

        private string fromZIP = "";
        public string FromZIP
        {
            get { return fromZIP; }
            set
            {
                SetProperty(ref fromZIP, value);
                SourceAddr.ZIP5 = fromZIP;
            }
        }

        private string fromPhone = "";
        public string FromPhone
        {
            get { return fromPhone; }
            set
            {
                SetProperty(ref fromPhone, value);
                SourceAddr.Phone = fromPhone;
            }
        }
        /////////////////////////////////////////////////////////////
        private string toCompanyName = "";
        public string ToCompanyName
        {
            get { return toCompanyName; }
            set
            {
                SetProperty(ref toCompanyName, value);
                cAddressBase.CompanyName = toCompanyName;
            }
        }

        private string toContactName = "";
        public string ToContactName
        {
            get { return toContactName; }
            set
            {
                SetProperty(ref toContactName, value);
                cAddressBase.Name = toContactName;
            }
        }

        private string toAddress1 = "";
        public string ToAddress1
        {
            get { return toAddress1; }
            set
            {
                SetProperty(ref toAddress1, value);
                cAddressBase.Address1 = toAddress1;
            }
        }

        private string toAddress2 = "";
        public string ToAddress2
        {
            get { return toAddress2; }
            set
            {
                SetProperty(ref toAddress2, value);
                cAddressBase.Address2 = toAddress2;
            }
        }

        private string toCity = "";
        public string ToCity
        {
            get { return toCity; }
            set
            {
                SetProperty(ref toCity, value);
                cAddressBase.City = toCity;
            }
        }

        private string toState = "";
        public string ToState
        {
            get { return toState; }
            set
            {
                SetProperty(ref toState, value);
                cAddressBase.State = toState;
            }
        }

        private string toZIP = "";
        public string ToZIP
        {
            get { return toZIP; }
            set
            {
                SetProperty(ref toZIP, value);
                cAddressBase.ZIP5 = toZIP;
            }
        }

        private string toPhone = "";
        public string ToPhone
        {
            get { return toPhone; }
            set
            {
                SetProperty(ref toPhone, value);
                cAddressBase.Phone = toPhone;
            }
        }

        private string toEmaile = "";
        public string ToEmaile
        {
            get { return toEmaile; }
            set { SetProperty(ref toEmaile, value); }
        }

        private bool toNotification = true;
        public bool ToNotification
        {
            get { return toNotification; }
            set { SetProperty(ref toNotification, value); }
        }
        /////////////////////////////////////////////////////////
        private string shipFromAddress = "";
        public string ShipFromAddress
        {
            get { return shipFromAddress; }
            set { SetProperty(ref shipFromAddress, value); }
        }

        private string shipToAddress = "";
        public string ShipToAddress
        {
            get { return shipToAddress; }
            set { SetProperty(ref shipToAddress, value); }
        }
        /////////////////////////////////////////////////////
        private double weigthLbs;
        public double WeigthLbs
        {
            get { return weigthLbs; }
            set { SetProperty(ref weigthLbs, value); }
        }

        private double weigthLOz = 0;
        public double WeigthLOz
        {
            get { return weigthLOz; }
            set { SetProperty(ref weigthLOz, value); }
        }

        private double weigtKg = 0;
        public double WeigthLKg
        {
            get { return weigtKg; }
            set { SetProperty(ref weigtKg, value); }
        }
        
        public double Oz
        {
            get
            {
                double tempWeigth = 0;
                if(WeigthLOz != 0 && WeigthLbs != 0)
                {
                    tempWeigth = WeigthLOz + (WeigthLbs * 16);
                }
                else if(WeigthLOz != 0)
                {
                    tempWeigth = WeigthLOz;
                }
                else if(WeigthLbs != 0)
                {
                    tempWeigth = WeigthLbs * 16;
                }
                else if (WeigthLKg != 0)
                {
                    tempWeigth = WeigthLKg * 35.274;
                }
                return tempWeigth;
            }
        }
        

        /////////////////////////////////////////////////////
        private double dLength = 0;
        public double DLength
        {
            get { return dLength; }
            set
            {
                SetProperty(ref dLength, value);
                cDimensions.Length = dLength;
            }
        }

        private double dWidth = 0;
        public double DWidth
        {
            get { return dWidth; }
            set
            {
                SetProperty(ref dWidth, value);
                cDimensions.Width = dWidth;
            }
        }

        private double dHeigh = 0;
        public double DHeigh
        {
            get { return dHeigh; }
            set
            {
                SetProperty(ref dHeigh, value);
                cDimensions.Heigh = dHeigh;
            }
        }
        //////////////////////////////////////////////////////
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

        private bool noValidate = true;
        public bool NoValidate
        {
            get { return noValidate; }
            set { SetProperty(ref noValidate, value); }
        }

        private decimal insuranceAmount = 0;
        public decimal InsuranceAmount
        {
            get { return insuranceAmount; }
            set { SetProperty(ref insuranceAmount, value); }
        }

        private bool signatureWaiver = true;
        public bool SignatureWaiver
        {
            get { return signatureWaiver; }
            set { SetProperty(ref signatureWaiver, value); }
        }

        private string orderNumber = "";
        public string OrderNumber
        {
            get { return orderNumber; }
            set { SetProperty(ref orderNumber, value); }
        }

        private string itemDescription = "";
        public string ItemDescription
        {
            get { return itemDescription; }
            set { SetProperty(ref itemDescription, value); }
        }

        private int labelsQty = 1;
        public int LabelsQty
        {
            get { return labelsQty; }
            set { SetProperty(ref labelsQty, value); }
        }

        /////////////////////////////////////////////////////

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

        public bool IsValid { get; set; }

        public string TypeShipeMethod { get; set; }

        private async void InitToAddress()
        {
            IsValid = false;
            if ((ToAddress1 != "" && ToAddress1 != null)  && (ToCity != "" && ToCity != null) && (ToState != "" && ToState != null) 
                && (ToZIP != "" && ToZIP != null)  && (ToPhone != "" && ToPhone != null) && (ToEmaile != "" && ToEmaile != null))
            {
                ShipToAddress = $"{ToAddress1}, {ToAddress2}, {ToCity}, {ToState}, {ToZIP}, {ToPhone}, {ToEmaile}";
            }
            else
            {
                ShipToAddress = "-------------------";
            }
        }

        public bool CheckToAddress()
        {
            bool isToAddress = false;
            if ((ToCity != "" && ToCity != null) && (ToState != "" && ToState != null)
                && (ToZIP != "" && ToZIP != null) && (ToPhone != "" && ToPhone != null))
            {
                isToAddress = true;
            }
            return isToAddress;
        }

        private async void InitFromAddress()
        {
            IsValid = false;
            Carrier = null;
            if ((FromCity != "" && FromCity != null) && (FromState != "" && FromState != null) 
                && (FromZIP != "" && FromZIP != null) && (FromPhone != "" && FromPhone != null))
            {
                ShipFromAddress = $"{FromAddress1}, {FromAddress2}, {FromCity}, {FromState}, {FromZIP}, {FromPhone}";
            }
            else
            {
                ShipFromAddress = "-------------------";
            }
        }

        public bool CheckFromAddress()
        {
            bool isFromAddress = false;
            Carrier = null;
            if ((FromCity != "" && FromCity != null) && (FromState != "" && FromState != null)
                && (FromZIP != "" && FromZIP != null) && (FromPhone != "" && FromPhone != null))
            {
                isFromAddress = true;
            }
            return isFromAddress;
        }

        private async void Init()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage());
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                FromCompanyName = _xzType[2];
                FromContactName = _xzType[5];
                FromAddress1 = _xzType[10].Remove(_xzType[10].IndexOf(','));
                FromAddress2 = _xzType[10].Remove(0, _xzType[10].IndexOf(',') + 2);
                FromCity = _xzType[12];
                FromState = _xzType[13];
                FromZIP = _xzType[14];
                FromPhone = _xzType[15];
                Balance = _xzType[18];
                InitFromAddress();
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("Error"), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
        }
        
        public async void DisplayShippingOptions()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            int stateAuth = 0;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            
            if(Oz != 0)
            {
                stateAuth = 5;
            }
            else if (Carrier == null)
            {
                stateAuth = managerVazoo.PrintingWork("Options", ref description, cDimensions, SourceAddr, cAddressBase,
                    Oz, SignatureConfirmation, DeliveryConfirmation, NoValidate, InsuranceAmount, ref carriers, email, idCompany, psw);
            }   
            else
            {
                stateAuth = 3;
            }
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                CarriersUSPS = new List<Carrier>(carriers.FindAll(c => c.Company == 1));
                CarriersUPS = new List<Carrier>(carriers.FindAll(c => c.Company == 2));
                CarriersFedEx = new List<Carrier>(carriers.FindAll(c => c.Company == 3));
                await PopupNavigation.PushAsync(new OptinsPage(this, CarriersUSPS.Count != 0, CarriersUPS.Count != 0, CarriersFedEx.Count != 0), true);
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
            else if(stateAuth == 5)
            {

            }
        }

        public async void ShippingCreate()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string tracking = null;
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
            else if(TypeShipeMethod == "UPS")
            {
                 shipingMethod = "UPS_" + carrier.Code;
            }
            else if(TypeShipeMethod == "FedEx")
            {
                 shipingMethod = "FedEx_" + carrier.Code;
            }
            int stateAuth = managerVazoo.ShippingCreate(Convert.ToInt32(idCompany), email, psw, LabelsQty, shipingMethod, ToEmaile, SignatureWaiver, Oz, cDimensions, SourceAddr, 
                cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, ToNotification, OrderNumber, ItemDescription, "", InsuranceAmount, ref tracking, ref description);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                if(tracking != null || tracking != "")
                {
                    await PopupNavigation.PushAsync(new LabalPageView(tracking), true);
                }
                else
                {
                    await PopupNavigation.PushAsync(new Compleat("Label successfully printed"), true);
                }
                
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