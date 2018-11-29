using Rg.Plugins.Popup.Services;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Mesages;
using Vazoo1123.Views.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Vazoo1123.ViewModels.Mesages.MesagesFolderMV;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoriMesage : ContentPage
	{
        MesagesFolderMV mesagesFolderMV = null;

        public HistoriMesage (ManagerVazoo managerVazoo, MenuDetalePage menuDetalePage)
		{
			InitializeComponent ();
            this.mesagesFolderMV = new MesagesFolderMV(managerVazoo, menuDetalePage);
            BindingContext = mesagesFolderMV;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PushAsync(new ModelFilterMesage(mesagesFolderMV), true);
        }

        private async void OrderList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Models.Messages messages = (Models.Messages)e.SelectedItem;
                await Navigation.PushAsync(new Conversation(mesagesFolderMV.managerVazoo, messages, mesagesFolderMV.initMesage));
                messageList.SelectedItem = null;
            }
        }

        private async void OrderList_Refreshing(object sender, System.EventArgs e)
        {
            mesagesFolderMV.InitMessages();
        }
    }
}