using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public delegate void InitMesage();
        public InitMesage initMesage;


        public MesagesFolderMV(ManagerVazoo managerVazoo, MenuDetalePage menuDetalePage)
        {
            this.managerVazoo = managerVazoo;
            this.menuDetalePage = menuDetalePage;
            Type = 1;
            Name = "Today";
            initMesage = InitMessages;
            InitMessages();
        }

        public List<Models.Messages> messagesss1 = null;

        private ObservableCollection<Models.Messages> messagesss = null;
        public ObservableCollection<Models.Messages> Messagesss
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
            get => type;
            set => SetProperty(ref type, value);
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private string name = null;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int countPage = 0;

        public async void InitMessages()
        {
            IsBusy = true;
            string description = null;
            int totalResulte = 0;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            countPage = 0;
            messagesss1 = null;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("MessagesGet", ref description, ref totalResulte, ref messagesss1, ref countPage, email, idCompany, psw, Type.ToString(), "", countPage.ToString());
            });
            if (stateAuth == 3)
            {
                Messagesss = new ObservableCollection<Models.Messages>(messagesss1.GetRange(0, 10 > messagesss1.Count ? messagesss1.Count : 10));
                Title = $"{name} {totalResulte}";
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

        public  void GetMessages()
        {
            IsBusy = true;
            string description = null;
            int totalResulte = 0;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            managerVazoo.MesagesWork("MessagesGet", ref description, ref totalResulte, ref messagesss1, ref countPage, email, idCompany, psw, (Type).ToString(), "", countPage.ToString());
            IsBusy = false;
        }
    }
}