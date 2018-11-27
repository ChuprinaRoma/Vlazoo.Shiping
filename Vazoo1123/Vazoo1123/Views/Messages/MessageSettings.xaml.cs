using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageSettings : PopupPage
    {
        private ConversationAndPurchasesMV conversationAndPurchasesMV = null;

        public MessageSettings (ConversationAndPurchasesMV conversationAndPurchasesMV)
		{
            this.conversationAndPurchasesMV = conversationAndPurchasesMV;
            InitializeComponent ();
            BindingContext = this.conversationAndPurchasesMV;
		}
	}
}