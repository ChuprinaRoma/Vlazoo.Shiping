using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Service;
using Vazoo1123.Views;
using Vazoo1123.Views.A_R.ModalView;
using Vazoo1123.Views.LoadViews;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels
{
    class RegistrationViewModels : BindableBase
    {
        public INavigation Navigation { get; set; }
        public DelegateCommand ToHelpeCommand { get; set; }
        public DelegateCommand ToRegistrationCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }
        private ManagerVazoo managerVazoo { get; set; }
        public DelegateCommand ToYoutubeCommand { get; set; }
        public DelegateCommand ToFaceBockCommand { get; set; }

        public RegistrationViewModels()
        {
            ToHelpeCommand = new DelegateCommand(ToHelpe);
            managerVazoo = new ManagerVazoo();
            BackCommand = new DelegateCommand(Back);
            ToRegistrationCommand = new DelegateCommand(ToRegistration);
            ToYoutubeCommand = new DelegateCommand(ToYoutube);
            ToFaceBockCommand = new DelegateCommand(ToFaceBock);
        }

        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string idCompany = "";
        public string IdCompany
        {
            get { return idCompany; }
            set { SetProperty(ref idCompany, value); }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string confirmPassword = "";
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }
        
        private async void ToHelpe()
        {
            await PopupNavigation.PushAsync(new ContexWindowHelp(), true);
        }

        private void Back()
        {
            Navigation.PopModalAsync(true);
        }

        private async void ToRegistration()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string description = null;
            int stateAuth = managerVazoo.A_RWork("RegistrationSt", ref description, UserName, IdCompany, Password);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new RegistrationConfirmation());
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
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

        private async void ToYoutube()
        {
            Device.OpenUri(new Uri("https://www.youtube.com/channel/UCBgNOYRYH3Wr9mcl3lHkqKg?view_as=subscriber"));
        }

        private async void ToFaceBock()
        {
            Device.OpenUri(new Uri("https://www.facebook.com/vlazoo2016/"));
        }
    }
}