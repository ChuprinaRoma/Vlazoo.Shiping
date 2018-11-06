using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;

namespace Vazoo1123.ViewModels.Profile
{
    class BuyPostageMW : BindableBase
    {
        private ManagerVazoo managerVazoo = null;

        public BuyPostageMW(ManagerVazoo managerVazoo)
        {
            this.managerVazoo = managerVazoo;
            Init();
        }

        private string balance;
        public string Balance
        {
            get { return balance; }
            set
            {
                if (value != "" || value != null)
                {
                    SetProperty(ref balance, value + "$");
                }
                else
                {
                    SetProperty(ref balance, "N/D");
                }
            }
        }

        private async void Init()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage());
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("PostageBuyGet", ref description, null, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                Balance = _xzType[1];
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("Error"), true);
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