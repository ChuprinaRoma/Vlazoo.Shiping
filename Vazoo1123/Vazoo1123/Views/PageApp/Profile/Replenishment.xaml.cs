using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Replenishment : ContentPage
	{
		public Replenishment (string url, string title)
		{
			InitializeComponent ();
            webView.Source = url;
            Title = title;
        }

        private void StackLayout_SizeChanged(object sender, System.EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            webView.HeightRequest = onePercentheigth * 99;
            webView.WidthRequest = onePercentwidth * 95;
        }
    }
}