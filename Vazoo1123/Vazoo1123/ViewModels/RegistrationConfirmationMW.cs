using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;

namespace Vazoo1123.ViewModels
{
    class RegistrationConfirmationMW : BindableBase
    {
        private ManagerVazoo managerVazoo = null;
        public DelegateCommand CheckCommand { get; set; }

        public RegistrationConfirmationMW()
        {
            managerVazoo = new ManagerVazoo();
            CheckCommand = new DelegateCommand(CheckCode);
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }

        private string warrning;
        public string Warrning
        {
            get { return warrning; }
            set { SetProperty(ref warrning, value); }
        }

        private async void CheckCode()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string description = null;
            int stateAuth = managerVazoo.A_RWork("RegistrationEn", ref description, Code);
            await PopupNavigation.PopAsync(false);
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat("Registration completed successfully! Login information has been sent to the e-mail"), true);
            }
            else if (stateAuth == 2)
            {
                Warrning = description;
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