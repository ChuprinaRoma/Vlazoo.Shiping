using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.NewElement;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.ModalView;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Vazoo1123.ViewModels.Dashbord.DashbordMW;

namespace Vazoo1123.Views.PageApp.Dashbord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BulkPostagePrinting : ContentPage
	{
        private BulkPostagePrintingMV bulkPostagePrintingMV = null;

        public BulkPostagePrinting (ManagerVazoo managerVazoo, List<OrderInfo> SelectProduct, InitDasbordDelegate initDasbord, DashbordMW dashbordMW)
		{
            bulkPostagePrintingMV = new BulkPostagePrintingMV(managerVazoo, SelectProduct, initDasbord, dashbordMW) { Navigation = this.Navigation};
            InitializeComponent ();
            BindingContext = bulkPostagePrintingMV;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((Button)sender).FindByName<StackLayout>("st");
            await stackLayout.FadeTo(0.2, 300);
            await PopupNavigation.PushAsync(new LoadPage());
            string itemId = ((Button)sender).FindByName<Label>("itemId").Text;
            string recordId = ((Button)sender).FindByName<Label>("recordId").Text;
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            fullOrderSettings.cDimensions.Height = Convert.ToDouble(fullOrderSettings.DimensionsH);
            fullOrderSettings.cDimensions.Width = Convert.ToDouble(fullOrderSettings.DimensionsW);
            fullOrderSettings.cDimensions.Length = Convert.ToDouble(fullOrderSettings.DimensionsL);
            int indexSelectProduct = bulkPostagePrintingMV.SelectProduct
                .FindIndex(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            var WOzCrEntry = ((Button)sender).FindByName<CrossEntry>("WOzCrEntry");
            var WLbsCrEntry = ((Button)sender).FindByName<CrossEntry>("WLbsCrEntry");
            var WkgCrEntry = ((Button)sender).FindByName<CrossEntry>("WkgCrEntry");
            if (fullOrderSettings.Oz != 0)
            {
                await Task.Run(() => bulkPostagePrintingMV.UpdateOneOrder(fullOrderSettings));
                await PopupNavigation.PopAllAsync();
                if (bulkPostagePrintingMV.CarriersFedEx.Count != 0 || bulkPostagePrintingMV.CarriersUPS.Count != 0 || bulkPostagePrintingMV.CarriersUSPS.Count != 0)
                {
                    await PopupNavigation.PushAsync(new OptinsPage1(bulkPostagePrintingMV, fullOrderSettings.CarriersUSPS.Count != 0, fullOrderSettings.CarriersUPS.Count != 0, fullOrderSettings.CarriersFedEx.Count != 0, indexSelectProduct), true);
                }
                else
                {
                    await PopupNavigation.PushAsync(new Error("Carrer does not exist or did not have time to boot"), true);
                }
                WOzCrEntry.TextColor = Color.FromHex("#2aa0ea");
                WLbsCrEntry.TextColor = Color.FromHex("#2aa0ea");
                WkgCrEntry.TextColor = Color.FromHex("#2aa0ea");
                ValidWeight(itemId, recordId);
            }
            else
            {
                WOzCrEntry.TextColor = Color.Red;
                WLbsCrEntry.TextColor = Color.Red;
                WkgCrEntry.TextColor = Color.Red;
            }
            await stackLayout.FadeTo(1, 250);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((Image)sender).FindByName<StackLayout>("st");
            await stackLayout.FadeTo(0.2, 300);
            string itemId = ((Image)sender).FindByName<Label>("itemId").Text;
            string recordId = ((Image)sender).FindByName<Label>("recordId").Text;
            var WOzCrEntry = ((Image)sender).FindByName<CrossEntry>("WOzCrEntry");
            var WLbsCrEntry = ((Image)sender).FindByName<CrossEntry>("WLbsCrEntry");
            var WkgCrEntry = ((Image)sender).FindByName<CrossEntry>("WkgCrEntry");
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            if (fullOrderSettings.Oz != 0)
            {
                WOzCrEntry.TextColor = Color.FromHex("#2aa0ea");
                WLbsCrEntry.TextColor = Color.FromHex("#2aa0ea");
                WkgCrEntry.TextColor = Color.FromHex("#2aa0ea");
            }
            else
            {
                WOzCrEntry.TextColor = Color.Red;
                WLbsCrEntry.TextColor = Color.Red;
                WkgCrEntry.TextColor = Color.Red;
            }
            await stackLayout.FadeTo(1, 250);
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
            double DHeigh = Convert.ToDouble(fullOrderSettings.DimensionsH != "" ? fullOrderSettings.DimensionsH : "0");
            double DWidth = Convert.ToDouble(fullOrderSettings.DimensionsW != "" ? fullOrderSettings.DimensionsW : "0");
            double DLength = Convert.ToDouble(fullOrderSettings.DimensionsL != "" ? fullOrderSettings.DimensionsL : "0");
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
            if (((CrossEntry)sender).Text == "")
            {
                ((CrossEntry)sender).Text = "0";
            }
            else if (e.OldTextValue != null && e.OldTextValue == "0")
            {
                ((CrossEntry)sender).Text = e.NewTextValue.Remove(0, 1);
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (bulkPostagePrintingMV.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(bulkPostagePrintingMV, bulkPostagePrintingMV.PostageTotal, await bulkPostagePrintingMV.GetAndSetPostageBalance(), Navigation, true, bulkPostagePrintingMV.SelectProduct.Select(s => s.Carrier).ToList(), null));
            }
        }

        private void WOzCrEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != null)
            {
                bulkPostagePrintingMV.IsValid = false;
            }

            if (((CrossEntry)sender).Text == "")
            {
                ((CrossEntry)sender).Text = "0";
            }
            else if (e.OldTextValue != null && e.OldTextValue == "0")
            {
                ((CrossEntry)sender).Text = e.NewTextValue.Remove(0, 1);
            }
        }

        private void ValidWeight(string itemId, string recordId)
        {
            FullOrderSettings fullOrderSettings = bulkPostagePrintingMV.SelectProduct
                .Find(s => s.EBayItemID == itemId && s.RecordNumber == recordId);
            if (fullOrderSettings.WeightOZ != "0" && fullOrderSettings.WeightLBS != "0")
            {
                fullOrderSettings.WeightKG = "0";
            }
            else if (fullOrderSettings.WeightOZ != "0")
            {
                fullOrderSettings.WeightKG = "0";
            }
            else if (fullOrderSettings.WeightLBS != "0")
            {
                fullOrderSettings.WeightKG = "0";
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != "")
            {
                await PopupNavigation.PushAsync(new LabalPageView(label.Text));
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (bulkPostagePrintingMV.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(bulkPostagePrintingMV, bulkPostagePrintingMV.PostageTotal, await bulkPostagePrintingMV.GetAndSetPostageBalance(), Navigation, false, bulkPostagePrintingMV.SelectProduct.Select(s => s.Carrier).ToList(), null));
            }
        }
    }
}