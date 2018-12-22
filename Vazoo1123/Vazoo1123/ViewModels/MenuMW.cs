using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.Views;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels
{
    public class MenuMW : BindableBase
    {
        public DelegateCommand ToHelpCommand { get; set; }
        public DelegateCommand ToYoutubeCommand { get; set; }
        public DelegateCommand ToFaceBockCommand { get; set; }
        public ManagerVazoo managerVazoo = null;

        public MenuMW()
        {
            managerVazoo = new ManagerVazoo();
            ToHelpCommand = new DelegateCommand(ToHelp);
            InitMessages();
            ToYoutubeCommand = new DelegateCommand(ToYoutube);
            ToFaceBockCommand = new DelegateCommand(ToFaceBock);
            NameProfile = CrossSettings.Current.GetValueOrDefault("userName", "N/D");
        }

        private string nameProfile;
        public string NameProfile
        {
            get { return nameProfile; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref nameProfile, value);
                }
                else
                {
                    SetProperty(ref nameProfile, CrossSettings.Current.GetValueOrDefault("userName", "N/D"));
                }
            }
        }

        private string countMesage;
        public string CountMesage
        {
            get { return countMesage; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref countMesage, value);
                }
                else
                {
                    SetProperty(ref countMesage, "0");
                }
            }
        }

        private string countDashbord;
        public string CountDashbord
        {
            get { return countDashbord; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref countDashbord, value);
                }
                else
                {
                    SetProperty(ref countDashbord, "0");
                }
            }
        }

        public async void CheckAndSetCountDashbord(string count)
        {
            await Task.Run(() =>
            {
                if (count != null && count != "")
                {
                    CountDashbord = count;
                }
            });
        }

        public async void CheckAndSetCountMessage(string count)
        {
            await Task.Run(() =>
            {
                if (count != null && count != "")
                {
                    CountMesage = count;
                }
            });
        }

        public async void InitMessages()
        {
            await Task.Run(() =>
            {
                string description = null;
                int totalResulte = 0;
                int temp = 0;
                string email = CrossSettings.Current.GetValueOrDefault("userName", "");
                string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
                string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
                int stateAuth = 0;
                List<Models.Messages> messagess = null;
                stateAuth = managerVazoo.MesagesWork("MesageCountToDay", ref description, ref totalResulte, ref messagess, ref temp, email, idCompany, psw, "1", "", "0");
                if (stateAuth == 3)
                {
                    CheckAndSetCountMessage(totalResulte.ToString());
                }
            });
        }

        private async void ToHelp()
        {
            await PopupNavigation.PushAsync(new ContexWindowHelp(), true);
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