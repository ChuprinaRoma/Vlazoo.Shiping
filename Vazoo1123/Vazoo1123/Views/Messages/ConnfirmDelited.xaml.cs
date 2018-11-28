using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnfirmDelited : PopupPage
    {
        ConversationAndPurchasesMV conversationAndPurchasesMV = null;

        public ConnfirmDelited (ConversationAndPurchasesMV conversationAndPurchasesMV)
		{
            this.conversationAndPurchasesMV = conversationAndPurchasesMV;
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            conversationAndPurchasesMV.DeletedMsg();
            await PopupNavigation.PopAsync(true);
        }
    }
}