using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
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
                if(messages.Sender == "eBay")
                {
                    await Navigation.PushAsync(new PageForeBay(messages.EBayURL, messages.Subject));
                }
                else
                {
                    await Navigation.PushAsync(new Conversation(mesagesFolderMV.managerVazoo, messages, mesagesFolderMV.initMesage));
                }
                messageList.SelectedItem = null;
            }
        }

        private async void OrderList_Refreshing(object sender, System.EventArgs e)
        {
            mesagesFolderMV.InitMessages();
        }

        private void OrderList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            bool isLoad = true;
            Task.Run(async () =>
            {
                if ((((Models.Messages)e.Item).ID == mesagesFolderMV.Messagesss[mesagesFolderMV.Messagesss.Count - 3].ID
                 || ((Models.Messages)e.Item).ID == mesagesFolderMV.Messagesss[mesagesFolderMV.Messagesss.Count - 1].ID)
                && mesagesFolderMV.Messagesss.Count >= 10)
                {
                    if (mesagesFolderMV.messagesss1.Count - 10 <= mesagesFolderMV.Messagesss.Count)
                    {
                        await Task.Run(() => mesagesFolderMV.GetMessages());
                    }
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        int countPageAdd = (mesagesFolderMV.Messagesss.Count / 10);
                        int pageOrMV = mesagesFolderMV.messagesss1.Count / 10;
                        int pageP = mesagesFolderMV.Messagesss.Count / 10;
                        int remainderOrMV = pageOrMV == pageP ? mesagesFolderMV.messagesss1.Count % 10 : 0;
                        if (mesagesFolderMV.messagesss1.Count != mesagesFolderMV.Messagesss.Count)
                        {
                            for (int i = (countPageAdd * 10); i < (countPageAdd * 10 + (remainderOrMV == 0 ? 10 : remainderOrMV)); i++)
                            {
                                mesagesFolderMV.Messagesss.Add(mesagesFolderMV.messagesss1[i]);
                            }
                        }
                        return false;
                    });
                }
            });
        }
    }
}