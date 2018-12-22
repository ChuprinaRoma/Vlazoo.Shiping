using System;
using Vazoo1123.ViewModels.A_RViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Vazoo1123.Views.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        public FirstPage()
        {
            InitializeComponent();
            BindingContext = new FirstPageViewModels() { Navigation = this.Navigation };
        }

        void OnSizeChanged(object sender, TappedEventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            image.HeightRequest = onePercentheigth * 25;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://cgi6.ebay.com/ws/eBayISAPI.dll?SolutionsDirectory&page=details&solutionId=705082875"));
        }
    }
}