using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderOnePinting : ContentPage
	{
        FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV = null;

        public OrderOnePinting (OrderInfo orderInfo, ManagerVazoo managerVazoo)
		{
            fullInfoOneOrderAndPrintingMV = new FullInfoOneOrderAndPrintingMV(orderInfo, managerVazoo);
			InitializeComponent ();
            BindingContext = fullInfoOneOrderAndPrintingMV;
            sw.IsToggled = false;
            confirmationL.Text = "Delivery Confirmation";
            fullInfoOneOrderAndPrintingMV.SignatureConfirmation = false;
            fullInfoOneOrderAndPrintingMV.DeliveryConfirmation = true;
        }

        private void Init()
        {
            if (fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH != "" && fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH != "0" &&
                fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW != "" && fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW != "0" &&
                fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL != "" && fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL != "0")
            {
                double UPS = (Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH.Replace('.', ',')) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW.Replace('.', ',')) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL.Replace('.', ','))) / 166;
                double USPS = (Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH.Replace('.', ',')) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW.Replace('.', ',')) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL.Replace('.', ','))) / 194;

                fullInfoOneOrderAndPrintingMV.StrCalc = $"(UPS: {UPS.ToString("0.00")}, USPS: {USPS.ToString("0.00")})";
            }
            else
            {
                fullInfoOneOrderAndPrintingMV.StrCalc = "";
            }
        }

        private async void sw_Toggled(object sender, ToggledEventArgs e)
        {
            await confirmationL.FadeTo(.1, 300, Easing.BounceOut);
            if (((Switch)sender).IsToggled == true)
            {
                confirmationL.Text = "Signature Confirmation";
                fullInfoOneOrderAndPrintingMV.SignatureConfirmation = true;
                fullInfoOneOrderAndPrintingMV.DeliveryConfirmation = false;
            }
            else
            {
                confirmationL.Text = "Delivery Confirmation";
                fullInfoOneOrderAndPrintingMV.SignatureConfirmation = false;
                fullInfoOneOrderAndPrintingMV.DeliveryConfirmation = true;
            }
            await confirmationL.FadeTo(1, 300);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            fullInfoOneOrderAndPrintingMV.TypeShipeMethod = "USPS";
            fullInfoOneOrderAndPrintingMV.DisplayShippingOptions();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new Conditions(), true);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (fullInfoOneOrderAndPrintingMV.IsValid)
            {
                fullInfoOneOrderAndPrintingMV.ShippingCreate();
                DSOBtn.BorderColor = Color.Default;
                DSOBtn.BorderWidth = 0;
            }
            else
            {
                DSOBtn.BorderColor = Color.Red;
                DSOBtn.BorderWidth = 1;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new EditShipToAddres(fullInfoOneOrderAndPrintingMV), true);
            fullInfoOneOrderAndPrintingMV.IsValid = false;
        }

        private void CrossEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Init();
        }
    }
}