using Rg.Plugins.Popup.Pages;
using System;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Purchases1 : PopupPage
    {
        ConversationAndPurchasesMV conversationAndPurchasesMV = null;

        public Purchases1(ConversationAndPurchasesMV conversationAndPurchasesMV)
		{
            this.conversationAndPurchasesMV = conversationAndPurchasesMV;
            InitializeComponent ();
            BindingContext = conversationAndPurchasesMV;
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = ((Label)sender);
            Device.OpenUri(new Uri($"https://www.ebay.com/itm/{label.Text}"));
        }
    }
}