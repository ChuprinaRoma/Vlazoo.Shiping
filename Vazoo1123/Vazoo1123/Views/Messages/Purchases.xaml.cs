using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Mesages;
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
	}
}