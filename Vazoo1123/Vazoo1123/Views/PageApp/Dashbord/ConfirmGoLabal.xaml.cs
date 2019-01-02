using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Vazoo1123.ViewModels.Dashbord;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmGoLabal : PopupPage
    {
        private DashbordMW dashbordMW = null;

        public ConfirmGoLabal (DashbordMW dashbordMW)
		{
            this.dashbordMW = dashbordMW;
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            dashbordMW.Type = 3;
            dashbordMW.UpdateOrder();
            await PopupNavigation.PopAsync(true);
        }

        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);

        }
    }
}