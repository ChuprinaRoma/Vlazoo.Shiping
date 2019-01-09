using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Models;
using Vazoo1123.NewElement;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.ModalView;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Vazoo1123.ViewModels.Dashbord.DashbordMW;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderOnePinting : ContentPage
    {
        FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV = null;

        public OrderOnePinting(OrderInfo orderInfo, ManagerVazoo managerVazoo, InitDasbordDelegate initDasbord, DashbordMW dashbordMW)
        {
            fullInfoOneOrderAndPrintingMV = new FullInfoOneOrderAndPrintingMV(orderInfo, managerVazoo, initDasbord, dashbordMW) { Navigation = this.Navigation};
            InitializeComponent();
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
                double UPS = Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL) / 166;
                double USPS = Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsH) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsW) * Convert.ToDouble(fullInfoOneOrderAndPrintingMV.OrderInfo.DimensionsL) / 194;

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
            ValidWeight();
            fullInfoOneOrderAndPrintingMV.DisplayShippingOptions();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (fullInfoOneOrderAndPrintingMV.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(fullInfoOneOrderAndPrintingMV, fullInfoOneOrderAndPrintingMV.Carrier.Price.ToString(), await fullInfoOneOrderAndPrintingMV.GetAndSetPostageBalance(), Navigation, true, null, fullInfoOneOrderAndPrintingMV.Carrier));
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
            if (e.OldTextValue != null)
            {
                fullInfoOneOrderAndPrintingMV.IsValid = false;
            }
            if (((CrossEntry)sender).Text == "")
            {
                ((CrossEntry)sender).Text = "0";
            }
            else if (e.OldTextValue != null && e.OldTextValue == "0")
            {
                ((CrossEntry)sender).Text = e.NewTextValue.Remove(0, 1);
            }
            Init();
        }

        private void ValidWeight()
        {
            if (fullInfoOneOrderAndPrintingMV.OrderInfo.WeightOZ != "0" && fullInfoOneOrderAndPrintingMV.orderInfo.WeightLBS != "0")
            {
                fullInfoOneOrderAndPrintingMV.OrderInfo.WeightKG = "0";
            }
            else if (fullInfoOneOrderAndPrintingMV.OrderInfo.WeightOZ != "0")
            {
                fullInfoOneOrderAndPrintingMV.OrderInfo.WeightKG = "0";
            }
            else if (fullInfoOneOrderAndPrintingMV.OrderInfo.WeightLBS != "0")
            {
                fullInfoOneOrderAndPrintingMV.OrderInfo.WeightKG = "0";
            }
        }

        private void OzInp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.OldTextValue != null)
            {
                fullInfoOneOrderAndPrintingMV.IsValid = false;
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

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != "")
            {
                await PopupNavigation.PushAsync(new LabalPageView(label.Text));
            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            if (fullInfoOneOrderAndPrintingMV.IsValid)
            {
                await PopupNavigation.PushAsync(new Confirm(fullInfoOneOrderAndPrintingMV, fullInfoOneOrderAndPrintingMV.Carrier.Price.ToString(), await fullInfoOneOrderAndPrintingMV.GetAndSetPostageBalance(), Navigation, false, null, fullInfoOneOrderAndPrintingMV.Carrier));
                DSOBtn.BorderColor = Color.Default;
                DSOBtn.BorderWidth = 0;
            }
            else
            {
                DSOBtn.BorderColor = Color.Red;
                DSOBtn.BorderWidth = 1;
            }
        }

        private void CrossEntry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != null)
            {
                fullInfoOneOrderAndPrintingMV.IsValid = false;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = ((Label)sender);
            Device.OpenUri(new Uri($"https://www.ebay.com/itm/{label.Text}"));
        }
    }
}