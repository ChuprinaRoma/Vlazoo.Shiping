using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Vazoo1123.Models;
using Vazoo1123.NewElement;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.ModalView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Vazoo1123.ViewModels.Dashbord.DashbordMW;

namespace Vazoo1123.Views.PageApp.Dashbord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BulkPostagePrinting : ContentPage
	{
        private BulkPostagePrintingMV bulkPostagePrintingMV = null;

        public BulkPostagePrinting (ManagerVazoo managerVazoo, List<OrderInfo> SelectProduct, InitDasbordDelegate initDasbord)
		{
            bulkPostagePrintingMV = new BulkPostagePrintingMV(managerVazoo, SelectProduct, initDasbord);
            InitializeComponent ();
            BindingContext = bulkPostagePrintingMV;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string itemId = ((Button)sender).FindByName<Label>("itemId").Text;
            string recordId = ((Button)sender).FindByName<Label>("recordId").Text;
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            int indexSelectProduct = bulkPostagePrintingMV.SelectProduct
                .FindIndex(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            bulkPostagePrintingMV.CarriersFedEx = fullOrderSettings.CarriersFedEx;
            bulkPostagePrintingMV.CarriersUPS = fullOrderSettings.CarriersUPS;
            bulkPostagePrintingMV.CarriersUSPS = fullOrderSettings.CarriersUSPS;
            if (bulkPostagePrintingMV.CarriersFedEx.Count != 0 || bulkPostagePrintingMV.CarriersUPS.Count != 0 || bulkPostagePrintingMV.CarriersUSPS.Count != 0)
            {
                await PopupNavigation.PushAsync(new OptinsPage1(bulkPostagePrintingMV, fullOrderSettings.CarriersUSPS.Count != 0, fullOrderSettings.CarriersUPS.Count != 0, fullOrderSettings.CarriersFedEx.Count != 0, indexSelectProduct), true);
            }
            else
            {
                await PopupNavigation.PushAsync(new Error("Carrer does not exist or did not have time to boot"), true);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((Image)sender).FindByName<StackLayout>("st");
            await stackLayout.FadeTo(0.2, 300);
            string itemId = ((Image)sender).FindByName<Label>("itemId").Text;
            string recordId = ((Image)sender).FindByName<Label>("recordId").Text;
            var WOzCrEntry = ((Image)sender).FindByName<CrossEntry>("WOzCrEntry");
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            if (fullOrderSettings.WeightOZ != 0.ToString() && fullOrderSettings.WeightOZ != "")
            {
                WOzCrEntry.TextColor = Color.FromHex("#2c4dff");
                WOzCrEntry.PlaceholderColor = Color.FromHex("#2c4dff");
                bulkPostagePrintingMV.UpdateOneOrder(itemId, recordId);
            }
            else
            {
                WOzCrEntry.PlaceholderColor = Color.Red;
                WOzCrEntry.TextColor = Color.Red;
            }
            await stackLayout.FadeTo(1, 250);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bulkPostagePrintingMV.IsBusy = true;
            if (bulkPostagePrintingMV.SelectProduct.Find(s => s.WeightOZ == "0" || s.WeightOZ == "" ) == null)
            {
                foreach (var order in bulkPostagePrintingMV.SelectProduct)
                {
                    await OrderList.FadeTo(0.2, 150);
                    bulkPostagePrintingMV.FullUpdateOrders(order);
                    await OrderList.FadeTo(1, 150);
                }
            }
            else
            {
                await PopupNavigation.PushAsync(new Error("Please fill in the 'Weight OZ' field"), true);
            }
            bulkPostagePrintingMV.IsBusy = false;
        }

        private void CrossEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != null)
            {
                bulkPostagePrintingMV.IsValid = false;
            }
            string itemId = ((CrossEntry)sender).FindByName<Label>("itemId").Text;
            string recordId = ((CrossEntry)sender).FindByName<Label>("recordId").Text;
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            double DHeigh = Convert.ToDouble(fullOrderSettings.DimensionsH != "" ? fullOrderSettings.DimensionsH.Replace('.', ',') : "0");
            double DWidth = Convert.ToDouble(fullOrderSettings.DimensionsW != "" ? fullOrderSettings.DimensionsW.Replace('.', ',') : "0");
            double DLength = Convert.ToDouble(fullOrderSettings.DimensionsL != "" ? fullOrderSettings.DimensionsL.Replace('.', ',') : "0");
            if (DHeigh != 0 && DWidth != 0 && DLength != 0)
            {
                double UPS = (DHeigh * DWidth * DLength) / 166;
                double USPS = (DHeigh * DWidth * DLength) / 194;
                bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId)
                .StrCalc = $"(UPS: {UPS.ToString("0.00")}, USPS: {USPS.ToString("0.00")})";
            }
            else
            {
                bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId).StrCalc = "";
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (bulkPostagePrintingMV.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(bulkPostagePrintingMV, bulkPostagePrintingMV.PostageTotal, bulkPostagePrintingMV.SelectProduct.Select(s => s.Carrier).ToList(), null));
            }
        }

        private void WOzCrEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != null)
            {
                bulkPostagePrintingMV.IsValid = false;
            }
        }
    }
}