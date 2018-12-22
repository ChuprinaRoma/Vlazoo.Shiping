using System;
using Vazoo1123.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordRecovery : ContentPage
	{
        PasswordRecoveryViewModels passwordRecoveryViewModels = null;
        public PasswordRecovery ()
		{
            passwordRecoveryViewModels = new PasswordRecoveryViewModels() { Navigation = this.Navigation };
            InitializeComponent();
            BindingContext = passwordRecoveryViewModels;
		}

        private void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            image.HeightRequest = onePercentheigth * 25;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://cgi6.ebay.com/ws/eBayISAPI.dll?SolutionsDirectory&page=details&solutionId=705082875"));
        }
    }
}