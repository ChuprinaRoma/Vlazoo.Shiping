using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using Vazoo1123.Service;
using Vazoo1123.Service.settings;
using Vazoo1123.Views;
using Vazoo1123.Views.A_R;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Menu;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels
{
    class AvthorizationViewModels : BindableBase
    {
        public DelegateCommand ToPasswordRecoveryCommand { get; set; }
        public INavigation Navigation { get; set; }
        public DelegateCommand ToHelpCommand { get; set; }
        public DelegateCommand AutorisationCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }
        private ManagerVazoo managerVazoo = null;

        public AvthorizationViewModels()
        {
            managerVazoo = new ManagerVazoo();
            BackCommand = new DelegateCommand(Back);
            ToPasswordRecoveryCommand = new DelegateCommand(ToPasswordRecovery);
            ToHelpCommand = new DelegateCommand(ToHelp);
            AutorisationCommand = new DelegateCommand(Authorisation);
        }

        private string idCompany = "";
        public string IdCompany
        {
            get { return idCompany; }
            set { SetProperty(ref idCompany, value); }
        }

        private string username = "";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        
        private void ToPasswordRecovery()
        {
            Navigation.PushModalAsync(new PasswordRecovery());
        }

        private async void ToHelp()
        {
            await PopupNavigation.PushAsync(new ContexWindowHelp(), true);
        }

        private void Back()
        {
            Navigation.PopModalAsync(true);
        }

        private async void Authorisation()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string description = null;
            int stateAuth = managerVazoo.A_RWork("authorisation", ref description, idCompany, username, password);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                CheckAuth.SavingAccount(IdCompany, Username, Password);
                Application.Current.MainPage = new MenuDetalePage();
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("Invalid login or password, please try again"), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
        }
    }
}