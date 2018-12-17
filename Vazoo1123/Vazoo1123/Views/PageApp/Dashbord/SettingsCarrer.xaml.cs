using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsCarrer : PopupPage
    {
        BulkPostagePrintingMV bulkPostagePrintingMV = null;
        public SettingsCarrer (BulkPostagePrintingMV bulkPostagePrintingMV)
		{
            this.bulkPostagePrintingMV = bulkPostagePrintingMV;
			InitializeComponent ();
            BindingContext = bulkPostagePrintingMV;
            confirmationL.Text = "Delivery Confirmation";
        }

        private async void sw_Toggled(object sender, ToggledEventArgs e)
        {
            await confirmationL.FadeTo(.1, 300, Easing.BounceOut);
            if (((Switch)sender).IsToggled == true)
            {
                confirmationL.Text = "Signature Confirmation";
                bulkPostagePrintingMV.SignatureConfirmation = true;
                bulkPostagePrintingMV.DeliveryConfirmation = false;
            }
            else
            {
                confirmationL.Text = "Delivery Confirmation";
                bulkPostagePrintingMV.SignatureConfirmation = false;
                bulkPostagePrintingMV.DeliveryConfirmation = true;
            }
            await confirmationL.FadeTo(1, 300);
        }
    }
}