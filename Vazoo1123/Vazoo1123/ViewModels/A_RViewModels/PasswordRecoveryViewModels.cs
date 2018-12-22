using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Service;
using Vazoo1123.Views;
using Vazoo1123.Views.LoadViews;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels
{
    class PasswordRecoveryViewModels : BindableBase
    {
        public INavigation Navigation { get; set; }
        public DelegateCommand ToHelpeCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand ToVlazoo_WebCommand { get; set; }
        public DelegateCommand RemindPasswordCommand { get; set; }
        public DelegateCommand ToYoutubeCommand { get; set; }
        public DelegateCommand ToFaceBockCommand { get; set; }

        ManagerVazoo managerVazoo = null;

        public PasswordRecoveryViewModels()
        {
            managerVazoo = new ManagerVazoo();
            BackCommand = new DelegateCommand(ToBack);
            ToHelpeCommand = new DelegateCommand(ToHelpe);
            ToVlazoo_WebCommand = new DelegateCommand(ToVlazoo_Web);
            RemindPasswordCommand = new DelegateCommand(RemindPassword);
            ToYoutubeCommand = new DelegateCommand(ToYoutube);
            ToFaceBockCommand = new DelegateCommand(ToFaceBock);
        }

        private string email = "";
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private void ToBack()
        {
            Navigation.PopModalAsync();
        }

        private async void ToHelpe()
        {
            await PopupNavigation.PushAsync(new ContexWindowHelp(), true);
        }

        private void ToVlazoo_Web()
        {
            Device.OpenUri(new Uri("https://vlazoo.com"));
        }

        private async void RemindPassword()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string description = null;
            int stateAuth = managerVazoo.A_RWork("RemindPassword", ref description, Email);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat("Instructions were sent to your email"), true);
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("Error in the Email field"), true);
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