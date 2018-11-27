using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Menu;

namespace Vazoo1123.ViewModels.Mesages
{
    public class MesagesFolderMV : BindableBase
    {
        public ManagerVazoo managerVazoo = null;
        public MenuDetalePage menuDetalePage = null;
        public delegate void UpdateMsg();
        public UpdateMsg updateMsg;

        public MesagesFolderMV(ManagerVazoo managerVazoo, MenuDetalePage menuDetalePage)
        {
            this.managerVazoo = managerVazoo;
            this.menuDetalePage = menuDetalePage;
            InitMessages(1, "ToDay");
        }

        private List<Models.Messages> messagesss = null;
        public List<Models.Messages> Messagesss
        {
            get => messagesss;
            set => SetProperty(ref messagesss, value);
        }

        private string title = null;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private int type = 1;
        public int Type
        {
            get => type - 1;
            set => SetProperty(ref type, value);
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public async void InitMessages(int type, string name)
        {
            IsBusy = true;
            string description = null;
            int totalResulte = 0;
            List<Models.Messages> messagess = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("MessagesGet", ref description, ref totalResulte, ref messagess, email, idCompany, psw, type.ToString(), "", "0");
            });
                if (stateAuth == 3)
            {
                //TranslationIntoFormatDate(messagess);
                Messagesss = messagess;
                Title = $"{name} {totalResulte}";
                Type = type;
                if(type == 1)
                {
                    menuDetalePage.CheckAndSetCountMessage(totalResulte);
                }
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
            IsBusy = false;
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