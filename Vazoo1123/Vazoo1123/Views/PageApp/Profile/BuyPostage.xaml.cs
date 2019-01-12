using Plugin.Settings;
using System;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyPostage : ContentPage
	{
        BuyPostageMW buyPostageMW = null;

        public BuyPostage (ManagerVazoo managerVazoo)
		{
            buyPostageMW = new BuyPostageMW(managerVazoo);
			InitializeComponent ();
            BindingContext = buyPostageMW;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            PaymentMethod.HeightRequest = onePercentwidth * 26;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            await Navigation.PushAsync(new Replenishment($"https://vlazoo.com/BuyPostagePP.aspx?ClientID={idCompany}&Amount={buyPostageMW.Postage}", "PayPal"));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            await Navigation.PushAsync(new Replenishment($"https://vlazoo.com/BuyPostageCC.aspx?ClientID={idCompany}&Amount={buyPostageMW.Postage}", "Visa or MasterCard"));
        }
    }
}