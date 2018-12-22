using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Printing.Models;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using static Vazoo1123.ViewModels.Dashbord.DashbordMW;

namespace Vazoo1123.ViewModels.Dashbord
{
    public class FullInfoOneOrderAndPrintingMV : BindableBase, ICreateShiping
    {
        private ManagerVazoo managerVazoo = null;
        private CAddressBase cAddressBase = null;
        private CAddressBase SourceAddr = null;
        private CDimensions cDimensions = null;
        private List<Carrier> carriers = null;
        public DelegateCommand EditCommand { get; set; }
        public INavigation Navigation { get; set; }
        InitDasbordDelegate initDasbord;

        public FullInfoOneOrderAndPrintingMV(OrderInfo orderInfo, ManagerVazoo managerVazoo, InitDasbordDelegate initDasbord)
        {
            carriers = new List<Carrier>();
            this.managerVazoo = managerVazoo;
            this.initDasbord = initDasbord;
            OrderInfo = orderInfo;
            Carrier = orderInfo.CarrierOptimal;
            if (Carrier != null)
            {
                IsValid = true;
                TypeShipeMethod = Carrier.CompanyName;
            }
            InitForFullInfoOrder(orderInfo.ShipToAddress);
            InitDisplayShippingOptions();
            InitForDisplayShippingOptions();

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

        private bool isValid = false;
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        public double Oz
        {
            get
            {
                double tempOz = 0;
                if ((OrderInfo.WeightOZ != "" && OrderInfo.WeightOZ != "0") && (OrderInfo.WeightLBS != "" && OrderInfo.WeightLBS != "0"))
                {
                    tempOz = Convert.ToDouble(OrderInfo.WeightOZ) + (Convert.ToDouble(OrderInfo.WeightLBS) * 16);
                }
                else if (OrderInfo.WeightOZ != "" && OrderInfo.WeightOZ != "0")
                {
                    tempOz = Convert.ToDouble(OrderInfo.WeightOZ);
                }
                else if (OrderInfo.WeightLBS != "" && OrderInfo.WeightLBS != "0")
                {
                    tempOz = Convert.ToDouble(OrderInfo.WeightLBS) * 16;
                }
                else if (OrderInfo.WeightKG != "" && OrderInfo.WeightKG != "0")
                {
                    tempOz = Convert.ToDouble(OrderInfo.WeightKG) * 35.274;
                }
                return tempOz;
            }
        }

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

        public async void InitDisplayShippingOptions()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            stateAuth = managerVazoo.ShippingEstimateOrderint("Options", OrderInfo.ID, ref description, ref carriers, email, idCompany, psw);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                CarriersUSPS = new List<Carrier>(carriers.FindAll(c => c.Company == 1));
                CarriersUPS = new List<Carrier>(carriers.FindAll(c => c.Company == 2));
                CarriersFedEx = new List<Carrier>(carriers.FindAll(c => c.Company == 3));
                CarriersUSPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersUPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersFedEx.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
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

        public async void DisplayShippingOptions()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            if (((CarriersFedEx != null && CarriersFedEx.Count != 0) || (CarriersUPS != null && CarriersUPS.Count != 0) || (CarriersUPS != null && CarriersUPS.Count != 0)) && !IsValid)
            {
                stateAuth = await InitForDisplayShippingOptions();
                if (stateAuth == 3)
                {
                    stateAuth = managerVazoo.PrintingWork("Options", ref description, cDimensions, SourceAddr, cAddressBase,
                       Oz, SignatureConfirmation, DeliveryConfirmation, NoValidate, 0, ref carriers, email, idCompany, psw);
                }
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

        private async Task<int> InitForDisplayShippingOptions()
        {
            cAddressBase = new CAddressBase();
            cDimensions = new CDimensions();
            SourceAddr = new CAddressBase();
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = null;
            await Task.Run(() =>
            {
                _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
            });
            int stateAuth = Convert.ToInt32(_xzType[0]);
            if (stateAuth == 3)
            {
                SourceAddr.CompanyName = _xzType[2];
                SourceAddr.Name = _xzType[5];
                SourceAddr.Address1 = _xzType[10].Remove(_xzType[10].IndexOf(','));
                SourceAddr.Address2 = _xzType[10].Remove(0, _xzType[10].IndexOf(',') + 2);
                SourceAddr.City = _xzType[12];
                SourceAddr.State = _xzType[13];
                SourceAddr.ZIP5 = _xzType[14];
                SourceAddr.Phone = _xzType[15];

                cDimensions.Heigh = Convert.ToDouble(OrderInfo.DimensionsH != "" ? OrderInfo.DimensionsH : "0");
                cDimensions.Width = Convert.ToDouble(OrderInfo.DimensionsW != "" ? OrderInfo.DimensionsW : "0");
                cDimensions.Length = Convert.ToDouble(OrderInfo.DimensionsL != "" ? OrderInfo.DimensionsL : "0");
                if (orderInfo.ShipToAddress.Address1 != "")
                {
                    cAddressBase.Address1 = orderInfo.ShipToAddress.Address1;
                    cAddressBase.Address2 = orderInfo.ShipToAddress.Address2;
                    cAddressBase.Address3 = orderInfo.ShipToAddress.Address3;
                }
                else if (orderInfo.ShipToAddress.Address2 != "")
                {
                    cAddressBase.Address1 = orderInfo.ShipToAddress.Address2;
                    cAddressBase.Address2 = orderInfo.ShipToAddress.Address1;
                    cAddressBase.Address3 = orderInfo.ShipToAddress.Address3;
                }
                else
                {
                    cAddressBase.Address1 = orderInfo.ShipToAddress.Address3;
                    cAddressBase.Address2 = orderInfo.ShipToAddress.Address2;
                    cAddressBase.Address3 = orderInfo.ShipToAddress.Address1;
                }
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
            string printerId = CrossSettings.Current.GetValueOrDefault("printer", "");
            string[] idOrNamePrinter = printerId.Split(',');
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
            if(idOrNamePrinter[0] == "Default Printer")
            {
                printerId = GetIdDefaultPrinter();
            }
            int stateAuth = managerVazoo.ShippingCreateOrder(Convert.ToInt32(idCompany), email, psw, OrderInfo.ID, LabelsQty, shipingMethod, OrderInfo.ShopperEmail, SignatureWaiver,
                Oz, cDimensions, SourceAddr, cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, true, "", "", printerId, 0, ref tracking, ref description);
            
            if (stateAuth == 3)
            {
                string trakingOrder = null;
                initDasbord.Invoke();
                await Navigation.PopAsync();
                    managerVazoo.DashbordWork("OrderGet", ref description, OrderInfo.ID, ref trakingOrder, email, idCompany, psw);
                Thread.Sleep(2000);
                await PopupNavigation.PopAllAsync();
                if (trakingOrder != null && trakingOrder != "")
                {
                    await PopupNavigation.PushAsync(new LabalPageView(trakingOrder));
                }
                else
                {
                    await PopupNavigation.PushAsync(new Compleat("Label successfully printed, look for label in \'Labels Printed Last 72h\'"), true);
                }
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PopAllAsync();
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PopAllAsync();
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PopAllAsync();
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

        private async void EditShipToAddres()
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}