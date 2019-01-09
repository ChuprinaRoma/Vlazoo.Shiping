using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.ViewModels.Mesages;
using Vazoo1123.Views.Printing.ModalViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Purchases : PopupPage
    {
        ConversationAndPurchasesMV conversationAndPurchasesMV = null;
        public Purchases(ConversationAndPurchasesMV conversationAndPurchasesMV)
		{
            this.conversationAndPurchasesMV = conversationAndPurchasesMV;
            InitializeComponent ();
            BindingContext = conversationAndPurchasesMV;
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != "")
            {
                await PopupNavigation.PushAsync(new LabalPageView(label.Text));
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != "")
            {
                Device.OpenUri(new Uri(conversationAndPurchasesMV.OrderInfo.TrackingURL[0]));
            }
        }
    }
}