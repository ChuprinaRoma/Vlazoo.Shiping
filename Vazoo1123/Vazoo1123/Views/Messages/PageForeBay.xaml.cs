using System;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageForeBay : ContentPage
	{
        private string eBayUrl = null;
        private ConversationAndPurchasesMV conversationAndPurchasesMV = null;

        public PageForeBay (string eBayUrl, string subject, ManagerVazoo managerVazoo, string messageID)
		{
            conversationAndPurchasesMV = new ConversationAndPurchasesMV(managerVazoo, null, null, messageID, true);
            this.eBayUrl = eBayUrl;
			InitializeComponent ();
            subj.Text = subject;
            BindingContext = conversationAndPurchasesMV;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(eBayUrl));
        }
    }
}