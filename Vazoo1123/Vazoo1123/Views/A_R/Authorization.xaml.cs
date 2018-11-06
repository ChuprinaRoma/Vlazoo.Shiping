using System;
using Vazoo1123.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Authorization : ContentPage
	{
        AvthorizationViewModels avthorizationViewModels = null;
        public Authorization ()
		{
            avthorizationViewModels = new AvthorizationViewModels()
            {
                Navigation = this.Navigation
            };
            InitializeComponent ();
            BindingContext = avthorizationViewModels;
        }
        
        private void OnImageNameTapped(object sender, EventArgs args)
        {
            if(psw.IsPassword)
            {
                psw.IsPassword = false;
            }
            else
            {
                psw.IsPassword = true;
            }
        }
        
        private void TextC_emaile(object sender, TextChangedEventArgs e)
        {

        }

        void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            image.HeightRequest = onePercentheigth * 25;
        }
    }
}