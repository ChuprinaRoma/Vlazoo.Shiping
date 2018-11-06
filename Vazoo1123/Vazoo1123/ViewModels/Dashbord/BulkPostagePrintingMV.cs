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

namespace Vazoo1123.ViewModels.Dashbord
{
    public class BulkPostagePrintingMV : BindableBase
    {
        private ManagerVazoo managerVazoo = null;
        public CAddressBase cAddressBase = null;
        public DelegateCommand GoToSettingsCommand { get; set; }

        public BulkPostagePrintingMV(ManagerVazoo managerVazoo, List<OrderInfo> selectProducts)
        {
            GoToSettingsCommand = new DelegateCommand(GoToSettings);
            cAddressBase = new CAddressBase();
            this.managerVazoo = managerVazoo;
            Init(selectProducts);
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
                cAddressBase.CompanyName = _xzType[2];
                cAddressBase.Name = _xzType[5];
                cAddressBase.Address1 = _xzType[10].Remove(_xzType[10].IndexOf(','));
                cAddressBase.Address2 = _xzType[10].Remove(0, _xzType[10].IndexOf(',') + 2);
                cAddressBase.City = _xzType[12];
                cAddressBase.State = _xzType[13];
                cAddressBase.ZIP5 = _xzType[14];
                cAddressBase.Phone = _xzType[15];
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
                Task.Run(() =>
                {
                    double tempPostage = 0;
                    foreach (var selectProduct1 in SelectProduct)
                    {
                        selectProduct1.SetCarrier(selectProduct1.CarrierOptimal);
                        tempPostage += selectProduct1.CarrierOptimal.Price;
                    }
                    PostageTotal = $"{tempPostage}$";
                });
            }
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
                    stateAuth = managerVazoo.PrintingWork("Options", ref description, order.cDimensions, order.SourceAddr, cAddressBase,
                           Convert.ToDouble(order.WeightOZ != "" ? order.WeightOZ.Replace(',', '.') : "0"), SignatureConfirmation,
                           DeliveryConfirmation, NoValidate, 0, ref order.carriers, email, idCompany, psw);
                });
                if (stateAuth == 3)
                {
                    order.CarriersUSPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 1));
                    order.CarriersUPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 2));
                    order.CarriersFedEx = new List<Carrier>(order.carriers.FindAll(c => c.Company == 3));
                }
                else if (stateAuth != 3)
                {
                    order.CarriersUSPS = new List<Carrier>();
                    order.CarriersUPS = new List<Carrier>();
                    order.CarriersFedEx = new List<Carrier>();
                }
            }
        }

        public async void FullUpdateOrders(FullOrderSettings order)
        {
            int stateAuth = 0;
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.PrintingWork("Options", ref description, order.cDimensions, order.SourceAddr, cAddressBase,
                       Convert.ToDouble(order.WeightOZ != "" ? order.WeightOZ.Replace(',', '.') : "0"), SignatureConfirmation,
                       DeliveryConfirmation, NoValidate, 0, ref order.carriers, email, idCompany, psw);
                Task.Run(() =>
                {
                    double tempPostage = 0;
                    foreach (var selectProduct1 in SelectProduct)
                    {
                        selectProduct1.SetCarrier(selectProduct1.CarrierOptimal);
                        tempPostage += selectProduct1.CarrierOptimal.Price;
                    }
                    PostageTotal = $"{tempPostage}$";
                });
            });
            if (stateAuth == 3)
            {
                order.CarriersUSPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 1));
                order.CarriersUPS = new List<Carrier>(order.carriers.FindAll(c => c.Company == 2));
                order.CarriersFedEx = new List<Carrier>(order.carriers.FindAll(c => c.Company == 3));
            }
            else if (stateAuth != 3)
            {
                order.CarriersUSPS = new List<Carrier>();
                order.CarriersUPS = new List<Carrier>();
                order.CarriersFedEx = new List<Carrier>();
            }
        }

        public async void UpdateOneOrder(string idItem, string RecordNum)
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            FullOrderSettings fullOrderSettings = SelectProduct.Find(s => s.EBayItemID == idItem && s.RecordNumber == RecordNum);
            await Task.Run(() =>
            {
                int stateAuth = managerVazoo.PrintingWork("Options", ref description, fullOrderSettings.cDimensions, fullOrderSettings.SourceAddr, cAddressBase,
                           Convert.ToDouble(fullOrderSettings.WeightOZ != "" ? fullOrderSettings.WeightOZ.Replace(',', '.') : "0"), SignatureConfirmation,
                           DeliveryConfirmation, NoValidate, 0, ref fullOrderSettings.carriers, email, idCompany, psw);
                if (stateAuth == 3)
                {
                    fullOrderSettings.CarriersUSPS = new List<Carrier>(fullOrderSettings.carriers.FindAll(c => c.Company == 1));
                    fullOrderSettings.CarriersUPS = new List<Carrier>(fullOrderSettings.carriers.FindAll(c => c.Company == 2));
                    fullOrderSettings.CarriersFedEx = new List<Carrier>(fullOrderSettings.carriers.FindAll(c => c.Company == 3));
                    Task.Run(() =>
                    {
                        double tempPostage = 0;
                        foreach (var selectProduct1 in SelectProduct)
                        {
                            selectProduct1.SetCarrier(selectProduct1.CarrierOptimal);
                            tempPostage += selectProduct1.CarrierOptimal.Price;
                        }
                        PostageTotal = $"{tempPostage}$";
                    });
                }
                else if (stateAuth != 3)
                {
                    fullOrderSettings.CarriersUSPS = new List<Carrier>();
                    fullOrderSettings.CarriersUPS = new List<Carrier>();
                    fullOrderSettings.CarriersFedEx = new List<Carrier>();
                }
            });
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
                int stateAuth = managerVazoo.ShippingCreate(Convert.ToInt32(idCompany), email, psw, serlectProduct.LabelsQty, shipingMethod, 
                    serlectProduct.ShopperEmail, SignatureWaiver, Convert.ToDouble(serlectProduct.WeightOZ != "" ? serlectProduct.WeightOZ.Replace(',', '.') : "0"), serlectProduct.cDimensions, serlectProduct.SourceAddr,
                    cAddressBase, DeliveryConfirmation, SignatureWaiver, NoValidate, true, "", "", "", 0, ref tracking, ref description);
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
            await PopupNavigation.PopAllAsync(true);
        }

        private async void GoToSettings()
        {
            await PopupNavigation.PushAsync(new SettingsCarrer(this), true);
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