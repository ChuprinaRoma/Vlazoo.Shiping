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
	public partial class Conversation : ContentPage
	{
        private ConversationAndPurchasesMV conversationMV = null;
        public Conversation (ManagerVazoo managerVazoo, Models.Messages messages)
		{
            conversationMV = new ConversationAndPurchasesMV(managerVazoo, messages);
			InitializeComponent ();
            BindingContext = conversationMV;
           // msgList.ScrollTo(new object(), ScrollToPosition.End, true);

        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
        }
    }
}