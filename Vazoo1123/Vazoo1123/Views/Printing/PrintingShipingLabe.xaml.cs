using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Printing;
using Vazoo1123.Views.ModalView;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Printing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrintingShipingLabe : ContentPage
	{
        private PrintingShipingLabeMW printingShipingLabeMW = null;

        public PrintingShipingLabe (ManagerVazoo managerVazoo)
		{
            printingShipingLabeMW = new PrintingShipingLabeMW(managerVazoo); 
            InitializeComponent ();
            BindingContext = printingShipingLabeMW;
            sw.IsToggled = false;
            confirmationL.Text = "Delivery Confirmation";
            printingShipingLabeMW.SignatureConfirmation = false;
            printingShipingLabeMW.DeliveryConfirmation = true;
        }

        private async void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            await confirmationL.FadeTo(.1, 300, Easing.BounceOut);
            if(((Switch)sender).IsToggled == true)
            {
                confirmationL.Text = "Signature Confirmation";
                printingShipingLabeMW.SignatureConfirmation = true;
                printingShipingLabeMW.DeliveryConfirmation = false;
            }
            else
            {
                confirmationL.Text = "Delivery Confirmation";
                printingShipingLabeMW.SignatureConfirmation = false;
                printingShipingLabeMW.DeliveryConfirmation = true;
            }
            await confirmationL.FadeTo(1, 300);
        }

        private async void ClicBtnIn(object s, EventArgs e)
        {
            await PopupNavigation.PushAsync(new ShipFromAddress(printingShipingLabeMW), true);
        }

        private async void ClicBtnOut(object s, EventArgs e)
        {
            await PopupNavigation.PushAsync(new ShipToAddress(printingShipingLabeMW), true);
        }

        private async void OnTappedLabel(object sender, EventArgs args)
        {
            await PopupNavigation.PushAsync(new Conditions(), true);
        }
        
        private void CrossEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(printingShipingLabeMW.DHeigh != 0 && printingShipingLabeMW.DWidth != 0 && printingShipingLabeMW.DLength != 0)
            {
                double UPS = (printingShipingLabeMW.DHeigh * printingShipingLabeMW.DWidth * printingShipingLabeMW.DLength) / 166;
                double USPS = (printingShipingLabeMW.DHeigh * printingShipingLabeMW.DWidth * printingShipingLabeMW.DLength) / 194;
                printingShipingLabeMW.StrCalc = $"(UPS: {UPS.ToString("0.00")}, USPS: {USPS.ToString("0.00")})";
            }
            else
            {
                printingShipingLabeMW.StrCalc = "";
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ValidOption();
        }

        private void ValidOption()
        {
            bool isToAddress = false;
            bool isFromAddress = false;
            bool isWeigthLOz = false;
            if (printingShipingLabeMW.CheckToAddress())
            {
                isToAddress = true;
                fillOutSender.BorderWidth = 0;
                fillOutSender.BorderColor = Color.Default;
            }
            else
            {
                isToAddress = false;
                fillOutSender.BorderWidth = 1;
                fillOutSender.BorderColor = Color.Red;
            }
            if (printingShipingLabeMW.CheckFromAddress())
            {
                isFromAddress = true;
                fillInSender.BorderWidth = 0;
                fillInSender.BorderColor = Color.Default;
            }
            else
            {
                isFromAddress = false;
                fillInSender.BorderWidth = 1;
                fillInSender.BorderColor = Color.Red;
            }
            if (printingShipingLabeMW.WeigthLOz != 0)
            {
                isWeigthLOz = true;
                ozInp.TextColor = Color.FromHex("#2aa0ea");
            }
            else
            {
                ozInp.TextColor = Color.Red;
                isWeigthLOz = false;
            }
            if (isToAddress && isFromAddress && isWeigthLOz)
            {
                printingShipingLabeMW.TypeShipeMethod = "USPS";
                printingShipingLabeMW.DisplayShippingOptions();
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (printingShipingLabeMW.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(printingShipingLabeMW, printingShipingLabeMW.Carrier.Price.ToString(), null, printingShipingLabeMW.Carrier));
                DSOBtn.BorderColor = Color.Default;
                DSOBtn.BorderWidth = 0;
            }
            else
            {
                DSOBtn.BorderColor = Color.Red;
                DSOBtn.BorderWidth = 1;
            }
        }
    }
}