﻿using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Postage = "10.00";
            Init();
            CheckPrintingApp();
        }

        private Carrier carrier = null;
        public Carrier Carrier
        {
            get { return carrier; }
            set { SetProperty(ref carrier, value); }
        }

        private string postageBalance1 = "0";
        public string PostageBalance1
        {
            get { return postageBalance1; }
            set { SetProperty(ref postageBalance1, "$"+value); }
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
                cDimensions.Height = dHeigh;
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

        private string orderNumber = "Order #";
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

        private string postage = "";
        public string Postage
        {
            get => postage;
            set => SetProperty(ref postage, value);
        }


        public bool IsValid { get; set; }

        public string TypeShipeMethod { get; set; }

        private string feedBack = "";
        public string FeedBack
        {
            get => feedBack;
            set => SetProperty(ref feedBack, value);
        }

        private string colorFeedBack = "#000000";
        public string ColorFeedBack
        {
            get => colorFeedBack;
            set => SetProperty(ref colorFeedBack, value);
        }

        public double Balance { get; set; }

        private async Task<bool> CheckPrintingApp()
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            bool isPrinting = false;
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.PrintingWork("PrintingAppStatus", ref description, ref isPrinting, email, idCompany, psw);
            });
            if (stateAuth == 3)
            {
                FeedBack = description;
                if (isPrinting)
                {
                    ColorFeedBack = "#01DF01";
                }
                else
                {
                    ColorFeedBack = "#DF0101";
                }
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
            return isPrinting;
        }

        private async void InitToAddress()
        {
            IsValid = false;
            if (ToContactName != "" && ToAddress1 != "" && ToCity != "" && ToState != "" && ToZIP != "" )
            {
                ShipToAddress = $"{ToContactName}, {ToAddress1}, {ToCity}, {ToState}, {ToZIP}";
            }
            else
            {
                ShipToAddress = "-------------------";
            }
            if (PopupNavigation.PopupStack != null && PopupNavigation.PopupStack.Count != 0)
            {
                await PopupNavigation.PopAsync(true);
            }
        }

        public bool CheckToAddress()
        {
            bool isToAddress = false;
            if (ToCity != "" && ToState != "" && ToContactName != ""  && ToZIP != "")
            {
                isToAddress = true;
            }
            return isToAddress;
        }

        private async void InitFromAddress()
        {
            IsValid = false;
            Carrier = null;
            if (FromCompanyName != "" && FromCity != "" && FromState != null && FromZIP != "" && FromAddress1 != "")
            {
                ShipFromAddress = $"{FromCompanyName}, {FromAddress1}, {FromCity}, {FromState}, {FromZIP}";
            }
            else
            {
                ShipFromAddress = "-------------------";
            }
            if (PopupNavigation.PopupStack != null && PopupNavigation.PopupStack.Count != 0)
            {
                await PopupNavigation.PopAsync(true);
            }
        }

        public bool CheckFromAddress()
        {
            bool isFromAddress = false;
            Carrier = null;
            if (FromAddress1 != "" && FromCity != "" && FromState != "" && FromZIP != "")
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
                PostageBalance1 = _xzType[18];
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
            if (Carrier == null)
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
                CarriersUSPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersUPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersFedEx.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
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
        }

        public async void ShippingCreate()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            bool isPrinted = await CheckPrintingApp();
            if (!isPrinted)
            {
                await PopupNavigation.PopAllAsync();
                await PopupNavigation.PushAsync(new Error(FeedBack), true);
                return;
            }
            string description = null;
            string tracking = null;
            string shipingMethod = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string printerId = CrossSettings.Current.GetValueOrDefault("printer", "");
            string[] idOrNamePrinter = printerId.Split(',');
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
            if (idOrNamePrinter[0] == "Default Printer")
            {
                printerId = GetIdDefaultPrinter();
            }
            int stateAuth = managerVazoo.ShippingCreate(Convert.ToInt32(idCompany), email, psw, LabelsQty, shipingMethod, ToEmaile, SignatureWaiver, Oz, cDimensions, SourceAddr, 
                cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, ToNotification, OrderNumber, ItemDescription, printerId, InsuranceAmount, ref tracking, ref description);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat("Label successfully printed"), true);
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description + ". Look for label in Herror Label"), true);
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

        private string GetIdDefaultPrinter()
        {
            List<string[]> dropDwnChooseRemovePrinters = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            managerVazoo.PrintingWork("OptionsGet", ref dropDwnChooseRemovePrinters, idCompany, email, psw);
            return dropDwnChooseRemovePrinters.Find(d => d[0] == "Default Printer")[1];
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

        public async void ShippingCreate1()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string tracking = null;
            string shipingMethod = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
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
            int stateAuth = managerVazoo.ShippingCreate(Convert.ToInt32(idCompany), email, psw, LabelsQty, shipingMethod, ToEmaile, SignatureWaiver, Oz, cDimensions, SourceAddr,
                cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, ToNotification, OrderNumber, ItemDescription, null, InsuranceAmount, ref tracking, ref description);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat("Label successfully printed"), true);
                await PopupNavigation.PushAsync(new LabalPageView(tracking));
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description + ". Look for label in Herror Label"), true);
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

        public async Task<double> GetAndSetPostageBalance()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string[] _xzType = null;
            int stateAuth = 0;
            await Task.Run(() =>
            {
                string description = null;
                string email = CrossSettings.Current.GetValueOrDefault("userName", "");
                string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
                string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
                _xzType = managerVazoo.PofiletWork("PostageBuyGet", ref description, null, idCompany, email, psw);
                stateAuth = Convert.ToInt32(_xzType[0]);
            });
            if (stateAuth == 3)
            {
                Balance = Convert.ToDouble(_xzType[1]);
            }
            await PopupNavigation.PopAsync(true);
            return Balance;
        }
    }
}