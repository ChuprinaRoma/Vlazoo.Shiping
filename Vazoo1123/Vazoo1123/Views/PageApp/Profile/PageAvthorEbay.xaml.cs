using System;
using Vazoo1123.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAvthorEbay : ContentPage
	{
        private ProfileMW profileMW = null;

        public PageAvthorEbay (string url, ProfileMW profileMW)
		{
			InitializeComponent ();
            this.profileMW = profileMW;
            webView.Source = url;
            webView.Navigating += OnChangeProperti;
        }

        private void OnChangeProperti(object s, WebNavigatingEventArgs e)
        {
            if(e.Url.Contains("https://vlazoo.com/login.aspx"))
            {
                profileMW.EBayConfirm();
                Navigation.PopAsync(true);
            }
        }


        void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            webView.HeightRequest = onePercentheigth * 99;
            webView.WidthRequest = onePercentwidth * 95;
        }
    }
}