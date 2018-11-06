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
		public BuyPostage (ManagerVazoo managerVazoo)
		{
			InitializeComponent ();
            BindingContext = new BuyPostageMW(managerVazoo);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            PaymentMethod.HeightRequest = onePercentwidth * 26;
        }
    }
}