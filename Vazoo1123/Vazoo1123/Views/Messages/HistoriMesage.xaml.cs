using Rg.Plugins.Popup.Services;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoriMesage : ContentPage
	{
        MesagesFolderMV mesagesFolderMV = null;

        public HistoriMesage (ManagerVazoo managerVazoo)
		{
			InitializeComponent ();
            this.mesagesFolderMV = new MesagesFolderMV(managerVazoo);
            BindingContext = mesagesFolderMV;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PushAsync(new ModelFilterMesage(mesagesFolderMV), true);
        }
    }
}