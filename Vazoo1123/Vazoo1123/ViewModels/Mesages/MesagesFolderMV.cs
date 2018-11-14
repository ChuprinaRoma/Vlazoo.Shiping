using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;

namespace Vazoo1123.ViewModels.Mesages
{
    public class MesagesFolderMV : BindableBase
    {
        private ManagerVazoo managerVazoo = null;

        public MesagesFolderMV(ManagerVazoo managerVazoo)
        {
            this.managerVazoo = managerVazoo;
            InitMessages(1);
        }

        private List<Models.Messages> messagesss = null;
        public List<Models.Messages> Messagesss
        {
            get => messagesss;
            set => SetProperty(ref messagesss, value);
        }

        private async void InitMessages(int type)
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            int totalResulte = 0;
            List<Models.Messages> messagess = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = managerVazoo.MesagesWork("MessagesGet", ref description, ref totalResulte, ref messagess, email, idCompany, psw, type.ToString(), "", "0");
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                //TranslationIntoFormatDate(messagess);
                Messagesss = messagess;
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
        }

        private List<Models.Messages> TranslationIntoFormatDate(List<Models.Messages> messagess1)
        {
            //foreach(var mesage in messagess1)
            //{
            //    mesage.ExpirationDate = mesage.ExpirationDate.Remove()
            //}
            
            DateTime dateTime = new DateTime(0,0,0,0,0,0,0);

            dateTime.AddTicks(636767136000000000);
            return null;
        }
    }
}