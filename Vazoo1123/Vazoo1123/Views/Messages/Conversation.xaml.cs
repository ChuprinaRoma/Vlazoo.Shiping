using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Vazoo1123.ViewModels.Mesages.MesagesFolderMV;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Conversation : ContentPage
	{
        private ConversationAndPurchasesMV conversationMV = null;

        public Conversation (ManagerVazoo managerVazoo, Models.Messages messages, InitMesage initMesage)
		{
            conversationMV = new ConversationAndPurchasesMV(managerVazoo, messages, initMesage);
            conversationMV.Navigation = Navigation;
            InitializeComponent ();
            BindingContext = conversationMV;

        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
        }

        private void MsgList_Refreshing(object sender, EventArgs e)
        {

        }
    }
}