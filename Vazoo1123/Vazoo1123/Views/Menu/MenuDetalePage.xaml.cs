using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Vazoo1123.Service.settings;
using Vazoo1123.ViewModels;
using Vazoo1123.Views.A_R;
using Vazoo1123.Views.Download_Application;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Messages;
using Vazoo1123.Views.PageApp;
using Vazoo1123.Views.PageApp.Profile;
using Vazoo1123.Views.Printing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetalePage : MasterDetailPage
    {
        private AbsoluteLayout BtnFocusInMenu { get; set; }
        private StackLayout BtnFocusInMenu1 { get; set; }
        MenuMW menuMW = null;
        private bool isMenu = true;
        public MenuDetalePage()
        {
            InitializeComponent();
            menuMW = new MenuMW();
            Detail = new NavigationPage(new DashboardPage(menuMW.managerVazoo, this));
            BtnFocusInMenu = new AbsoluteLayout();
            BindingContext = menuMW;
        }

        private void AnimateBtn(object s, EventArgs e)
        {
            BtnFocusInMenu.BackgroundColor = Color.White;
            BtnFocusInMenu1 = (StackLayout)s;
            BtnFocusInMenu.BackgroundColor = Color.Azure;
        }

        private void ToDashbord(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            Detail = new NavigationPage(new DashboardPage(menuMW.managerVazoo, this));
        }

        private void ToMessges(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            Detail = new NavigationPage(new HistoriMesage(menuMW.managerVazoo, this));
        }

        private void ToPrintingShiping(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            Detail = new NavigationPage(new PrintingShipingLabe(menuMW.managerVazoo));
        }

        private async void ToToProfile(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            await PopupNavigation.PushAsync(new LoadPage(), true);
            Detail = new NavigationPage(new ProfileV(menuMW.managerVazoo));
        }

        private async void ToBuyPostage(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            Detail = new NavigationPage(new BuyPostage(menuMW.managerVazoo));
        }

        private void SinOut(object s, EventArgs e)
        {
            Application.Current.MainPage = new FirstPage();
            Task.Run(() => CheckAuth.RmovegAccount());
            IsPresented = false;
        }

        private void ToDownLoadApp(object s, EventArgs e)
        {
            IsPresented = false;
            AnimateBtn(s, e);
            Detail = new NavigationPage(new DownloadApp());
        }

        public void CheckAndSetCountDashbord(int count)
        {
            menuMW.CheckAndSetCountDashbord(count.ToString());
        }

        public void CheckAndSetCountMessage(int count)
        {
            menuMW.CheckAndSetCountMessage(count.ToString());
        }

        private void Body_SizeChanged(object sender, EventArgs e)
        {
            double height = Application.Current.MainPage.Height;
            double Width = mainMenu.Width;
            body.HeightRequest = height - 20;
            countDasbord.Margin = new Thickness(mainMenu.Height - 108, 0, 0, 0);
        }
    }
}