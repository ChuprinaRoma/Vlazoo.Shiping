using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.PageApp.Dashbord;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using static Vazoo1123.ViewModels.Dashbord.DashbordMW;

namespace Vazoo1123.ViewModels.Dashbord
{
    public class BulkPostagePrintingMV : BindableBase, ICreateShiping
    {
        private ManagerVazoo managerVazoo = null;
        public CAddressBase SourceAddr = null;
        public DelegateCommand GoToSettingsCommand { get; set; }
        private InitDasbordDelegate initDasbord;
        public INavigation Navigation = null;

        public BulkPostagePrintingMV(ManagerVazoo managerVazoo, List<OrderInfo> selectProducts, InitDasbordDelegate initDasbord)
        {
            GoToSettingsCommand = new DelegateCommand(GoToSettings);
            SourceAddr = new CAddressBase();
            this.initDasbord = initDasbord;
            this.managerVazoo = managerVazoo;
            Init(selectProducts);
            CheckPrintingApp();
        }

        private async void Init(List<OrderInfo> selectProducts)
        {
            TotalLabels = selectProducts.Count.ToString();
            await Task.Run(() =>
            {
                InitSettingOrders(selectProducts);
                InitBaseAddres();
                InitDisplayShippingOptions();
            });
        }

        private int InitBaseAddres()
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
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
            }
            return stateAuth;
        }

        private void InitSettingOrders(List<OrderInfo> selectProducts)
        {
            SelectProduct = new List<FullOrderSettings>();
            foreach (var selectProduct in selectProducts)
            {
                FullOrderSettings fullOrderSettings = new FullOrderSettings(selectProduct);
                fullOrderSettings.CarrierOptimal = selectProduct.CarrierOptimal;
                fullOrderSettings.DateCreated = selectProduct.DateCreated;
                fullOrderSettings.DimensionsH = selectProduct.DimensionsH;
                fullOrderSettings.DimensionsL = selectProduct.DimensionsL;
                fullOrderSettings.DimensionsW = selectProduct.DimensionsW;
                fullOrderSettings.EBayItemID = selectProduct.EBayItemID;
                fullOrderSettings.EBayUserID = selectProduct.EBayUserID;
                fullOrderSettings.ID = selectProduct.ID;
                fullOrderSettings.ImageFile = selectProduct.ImageFile;
                fullOrderSettings.IsAutoPrint = selectProduct.IsAutoPrint;
                fullOrderSettings.ItemTitle = selectProduct.ItemTitle;
                fullOrderSettings.PaymentMethod = selectProduct.PaymentMethod;
                fullOrderSettings.QuantityPurchased = selectProduct.QuantityPurchased;
                fullOrderSettings.RecordNumber = selectProduct.RecordNumber;
                fullOrderSettings.ShipToAddress = selectProduct.ShipToAddress;
                fullOrderSettings.ShopperEmail = selectProduct.ShopperEmail;
                fullOrderSettings.SKU = selectProduct.SKU;
                fullOrderSettings.TrackingNumber = selectProduct.TrackingNumber;
                fullOrderSettings.TransactionPrice = selectProduct.TransactionPrice;
                fullOrderSettings.UPC = selectProduct.UPC;
                fullOrderSettings.WeightKG = selectProduct.WeightKG;
                fullOrderSettings.WeightLBS = selectProduct.WeightLBS;
                fullOrderSettings.WeightOZ = selectProduct.WeightOZ;
                SelectProduct.Add(fullOrderSettings);
            }
            Task.Run(() =>
            {
                bool isValidTemp = true;
                double tempPostage = 0;
                foreach (var selectProduct1 in SelectProduct)
                {
                    if(isValidTemp)
                    {
                        isValidTemp = selectProduct1.CarrierOptimal != null;
                    }
                    selectProduct1.SetCarrier(selectProduct1.CarrierOptimal);
                    tempPostage += selectProduct1.CarrierOptimal.Price;
                }
                IsValid = isValidTemp;
                PostageTotal = $"${tempPostage}";
            });
        }

        public async void InitDisplayShippingOptions()
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            foreach(var order in SelectProduct)
            {
                int stateAuth = 0;
                await Task.Run(() =>
                {
                    stateAuth = managerVazoo.ShippingEstimateOrderint("Options", order.ID, ref description, ref order.carriers, email, idCompany, psw);
                });
                if (stateAuth == 3)
                {
                    IsValid = true;
                    order.CarriersUSPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 1));
                    order.CarriersUPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 2));
                    order.CarriersFedEx = new List<Carrier>(order.carriers.FindAll(c => c.Company == 3));
                    order.CarriersUSPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                    order.CarriersUPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                    order.CarriersFedEx.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                }
                else if (stateAuth != 3)
                {
                    IsValid = false;
                    order.CarriersUSPS = new List<Carrier>();
                    order.CarriersUPS = new List<Carrier>();
                    order.CarriersFedEx = new List<Carrier>();
                }
            }
        }

        public async void UpdateOneOrder(FullOrderSettings fullOrderSettings)
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            List<Carrier> carriers = null;
            int stateAuth = managerVazoo.PrintingWork("Options", ref description, fullOrderSettings.cDimensions, SourceAddr, fullOrderSettings.cAddressBase,
                       fullOrderSettings.Oz, SignatureConfirmation,
                       DeliveryConfirmation, NoValidate, 0, ref carriers, email, idCompany, psw);
            if (stateAuth == 3)
            {
                isValid = true;
                fullOrderSettings.carriers = carriers;
                CarriersUSPS = new List<Carrier>(carriers.FindAll(c => c.Company == 1));
                CarriersUPS = new List<Carrier>(carriers.FindAll(c => c.Company == 2));
                CarriersFedEx = new List<Carrier>(carriers.FindAll(c => c.Company == 3));
                CarriersUSPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersUPS.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));
                CarriersFedEx.Sort((c1, c2) => c1.Price.CompareTo(c2.Price));

                double tempPostage = 0;
                foreach (var selectProduct1 in SelectProduct)
                {
                    selectProduct1.SetCarrier(selectProduct1.Carrier);
                    tempPostage += selectProduct1.Carrier.Price;
                }
                PostageTotal = $"${tempPostage}";
            }
        }

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
            string tracking = null; ;
            string shipingMethod = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string printerId = CrossSettings.Current.GetValueOrDefault("printer", "");
            string[] idOrNamePrinter = printerId.Split(',');
            int stait = 0;
            if (idOrNamePrinter[0] == "Default Printer")
            {
                printerId = GetIdDefaultPrinter();
            }
            foreach (var serlectProduct in SelectProduct)
            {
                if (serlectProduct.TypeShipeMethod == "USPS")
                {
                    shipingMethod = "USPS_" + serlectProduct.Carrier.AccountID + "=" + serlectProduct.Carrier.Code + "=" + (serlectProduct.Carrier.IsL5 ? "Y" : serlectProduct.Carrier.IsShippo ? "S" : "N")
                       + "=" + serlectProduct.Carrier.RateID + "=" + serlectProduct.Carrier.Cost.ToString("f") + "=" + serlectProduct.Carrier.Price.ToString("f");
                }
                else if (serlectProduct.TypeShipeMethod == "UPS")
                {
                    shipingMethod = "UPS_" + serlectProduct.Carrier.Code;
                }
                else if (serlectProduct.TypeShipeMethod == "FedEx")
                {
                    shipingMethod = "FedEx_" + serlectProduct.Carrier.Code;
                }
                int stateAuth = managerVazoo.ShippingCreateOrder(Convert.ToInt32(idCompany), email, psw, serlectProduct.ID, serlectProduct.QuantityPurchased, shipingMethod, 
                    serlectProduct.ShopperEmail, SignatureWaiver, serlectProduct.Oz, serlectProduct.cDimensions, SourceAddr,
                    serlectProduct.cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, true, "", "", printerId, 0, ref tracking, ref description);
                if (stateAuth == 3)
                {
                    stait = 3;
                    initDasbord.Invoke();
                }
                else if (stateAuth == 2)
                {
                    stait = 2;
                    await PopupNavigation.PushAsync(new Error(description + ". Look for label in Herror Label"), true);
                    break;
                }
                else if (stateAuth == 1)
                {
                    stait = 1;
                    await PopupNavigation.PushAsync(new Error(description), true);
                    break;
                }
                else if (stateAuth == 4)
                {
                    stait = 4;
                    await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
                    break;
                }
            }
            if (stait == 3)
            {
                await PopupNavigation.PushAsync(new Compleat($"Print Succefull"), true);
                await Navigation.PopAsync(true);
            }
        }

        public async void ShippingCreate1()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            string tracking = null; ;
            string shipingMethod = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string tracingAll = null;
            int stait = 0;
            foreach (var serlectProduct in SelectProduct)
            {
                if (serlectProduct.TypeShipeMethod == "USPS")
                {
                    shipingMethod = "USPS_" + serlectProduct.Carrier.AccountID + "=" + serlectProduct.Carrier.Code + "=" + (serlectProduct.Carrier.IsL5 ? "Y" : serlectProduct.Carrier.IsShippo ? "S" : "N")
                       + "=" + serlectProduct.Carrier.RateID + "=" + serlectProduct.Carrier.Cost.ToString("f") + "=" + serlectProduct.Carrier.Price.ToString("f");
                }
                else if (serlectProduct.TypeShipeMethod == "UPS")
                {
                    shipingMethod = "UPS_" + serlectProduct.Carrier.Code;
                }
                else if (serlectProduct.TypeShipeMethod == "FedEx")
                {
                    shipingMethod = "FedEx_" + serlectProduct.Carrier.Code;
                }
                int stateAuth = managerVazoo.ShippingCreateOrder(Convert.ToInt32(idCompany), email, psw, serlectProduct.ID, serlectProduct.QuantityPurchased, shipingMethod,
                    serlectProduct.ShopperEmail, SignatureWaiver, serlectProduct.Oz, serlectProduct.cDimensions, SourceAddr,
                    serlectProduct.cAddressBase, DeliveryConfirmation, SignatureConfirmation, NoValidate, true, "", "", null, 0, ref tracking, ref description);
                if (stateAuth == 3)
                {
                    stait = 3;
                    initDasbord.Invoke();
                    if (tracingAll != null)
                    {
                        tracingAll += "," + tracking;
                    }
                    else
                    {
                        tracingAll = tracking;
                    }
                }
                else if (stateAuth == 2)
                {
                    await PopupNavigation.PushAsync(new Error(description + ". Look for label in Herror Label"), true);
                    stait = 2;
                    break;
                }
                else if (stateAuth == 1)
                {
                    await PopupNavigation.PushAsync(new Error(description), true);
                    stait = 1;
                    break;
                }
                else if (stateAuth == 4)
                {
                    await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
                    stait = 4;
                    break;
                }
            }
            if (stait == 3)
            {
                await PopupNavigation.PushAsync(new LabalPageView(tracingAll));
                await Navigation.PopAsync(true);
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

        private async void GoToSettings()
        {
            await PopupNavigation.PushAsync(new SettingsCarrer(this), true);
        }
        
        private bool isValid = false;
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        private string postageTotal = "0";
        public string PostageTotal
        {
            get { return postageTotal; }
            set { SetProperty(ref postageTotal, value); }
        }

        private string totalLabels = "1";
        public string TotalLabels
        {
            get { return totalLabels; }
            set { SetProperty(ref totalLabels, value); }
        }

        private List<FullOrderSettings> selectProduct = null;
        public List<FullOrderSettings> SelectProduct
        {
            get { return selectProduct; }
            set { SetProperty(ref selectProduct, value); }
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
    }
}