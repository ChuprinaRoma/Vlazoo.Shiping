using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Messages;
using Xamarin.Forms;
using static Vazoo1123.ViewModels.Mesages.MesagesFolderMV;

namespace Vazoo1123.ViewModels.Mesages
{
    public class ConversationAndPurchasesMV : BindableBase
    {
        public ManagerVazoo managerVazoo = null;
        public DelegateCommand ToPurchasesCommand { get; set; }
        public DelegateCommand ToSettingsCommand { get; set; }
        public DelegateCommand SendOrRaplyCommand { get; set; }
        public DelegateCommand DeletedMsgCommand { get; set; }
        public DelegateCommand RefreshMessageCommand { get; set; }
        InitMesage initMesage;
        public INavigation Navigation { get; set; } 

        public ConversationAndPurchasesMV(ManagerVazoo managerVazoo, InitMesage initMesage,  Models.Messages messages = null, string mesageID = null)
        {
            this.managerVazoo = managerVazoo;
            ToPurchasesCommand = new DelegateCommand(ToPurchases);
            ToSettingsCommand = new DelegateCommand(ToSettings);
            SendOrRaplyCommand = new DelegateCommand(SendOrRaply);
            DeletedMsgCommand = new DelegateCommand(Confirm);
            RefreshMessageCommand = new DelegateCommand(InitConversation);
            this.initMesage = initMesage;
            if(messages != null)
            {
                MessagesID = messages.ID.ToString();
            }
            else
            {
                MessagesID = mesageID;
            }
            InitConversation();
            InitPurchases();
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private bool isEnambleSend = true;
        public bool IsEnambleSend
        {
            get => isEnambleSend;
            set => SetProperty(ref isEnambleSend, value);
        }

        private bool displayToPublic = false;
        public bool DisplayToPublic
        {
            get => displayToPublic;
            set => SetProperty(ref displayToPublic, value);
        }

        private bool emailCopyToSender = false;
        public bool EmailCopyToSender
        {
            get => emailCopyToSender;
            set => SetProperty(ref emailCopyToSender, value);
        }
        
        private string msg = "";
        public string Msg
        {
            get => msg;
            set => SetProperty(ref msg, value);
        }

        private Models.Messages messages = null;
        public Models.Messages Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        private string messagesID = null;
        public string MessagesID
        {
            get => messagesID;
            set => SetProperty(ref messagesID, value);
        }

        private List<Models.Messages> messagess = null;
        public List<Models.Messages> Messagess
        {
            get => messagess;
            set => SetProperty(ref messagess, value);
        }

        private OrderInfo orderInfo = null;
        public OrderInfo OrderInfo
        {
            get => orderInfo;
            set => SetProperty(ref orderInfo, value);
        }

        private Carrier carrier = null;
        public Carrier Carrier
        {
            get => carrier;
            set => SetProperty(ref carrier, value);
        }

        private Listings listings = null;
        public Listings Listings
        {
            get => listings;
            set => SetProperty(ref listings, value);
        }

        private async void InitPurchases()
        {
            IsBusy = true;
            string description = null;
            int totalResulte = 0;
            OrderInfo orderInfo = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("Purchases", ref totalResulte, ref orderInfo, email, idCompany, psw, MessagesID, "0");
            });
            if (stateAuth == 3)
            {
                OrderInfo = orderInfo;
                if (OrderInfo != null)
                {
                    Carrier = OrderInfo.CarrierOptimal;
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

        private async void InitConversation()
        {
            IsBusy = true;
            List<Models.Messages> messagesss = null;
            string description = null;
            int totalResulte = 0;
            OrderInfo orderInfo = null;
            int temp = 0;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("Conversation", ref description, ref totalResulte, ref messagesss, ref temp, email, idCompany, psw, MessagesID);
                if(messagesss == null)
                {
                    messagesss = new List<Messages>();
                }
            });
            if (stateAuth == 3)
            {
                await Task.Run(() =>
                {
                    managerVazoo.MesagesWork("MessageSetRead", Convert.ToInt32(MessagesID), ref description, email, idCompany, psw, Msg);
                });
                initMesage?.Invoke();
                Messagess = messagesss;
                if (Messagess.Count != 0)
                {
                    RemoveMessegesAllTeg();
                    IsEnambleSend = true;
                }
                else
                {
                    IsEnambleSend = false;
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

        private async void RemoveMessegesAllTeg()
        {
            await Task.Run(() =>
            {
                //Messagess
                foreach(var messages1 in Messagess)
                {
                    while (messages1.BodyShort.Contains("<br />"))
                    {
                        messages1.BodyShort = messages1.BodyShort.Replace("<br />", string.Empty);
                    }
                }
            });    
        }

        private async void ToPurchases()
        {
            if (Messagess != null)
            {
                if (OrderInfo != null)
                {
                    await PopupNavigation.PushAsync(new Purchases(this));
                }
                else
                {
                    ListingsGet();
                }
            }
            else
            {
                await PopupNavigation.PushAsync(new Error("Wait load messages"));
            }
        }

        private async void ToSettings()
        {
            await PopupNavigation.PushAsync(new MessageSettings(this));
        }

        private async void ListingsGet()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string description = null;
            int totalResulte = 0;
            Listings listings1 = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("ListingsGet", new List<string>() { Messagess[0].EBayItemID}, ref description, ref listings1, idCompany, email,  psw);
            });
            await PopupNavigation.PopAsync();
            if (stateAuth == 3)
            {
                Listings = listings1;
                await PopupNavigation.PushAsync(new Purchases1(this));
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

        private async void SendOrRaply()
        {
            IsBusy = true;
            string description = null;
            int totalResulte = 0;
            OrderInfo orderInfo = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("SendMessageReply", Convert.ToInt32(MessagesID), DisplayToPublic, EmailCopyToSender, ref description, email, idCompany, psw, Msg);
            });
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat("Send Successfully"), true);
                initMesage?.Invoke();
                await Navigation.PopAsync(true);

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

        private async void Confirm()
        {
            if (IsEnambleSend)
            {
                await PopupNavigation.PushAsync(new ConnfirmDelited(this));
            }
        }

        public async void DeletedMsg()
        {
            string description = null;
            int totalResulte = 0;
            OrderInfo orderInfo = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.MesagesWork("MessageDelete", Convert.ToInt32(MessagesID), ref description, email, idCompany, psw, Msg);
                initMesage.Invoke();
            });
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat(""), true);
                await Navigation.PopAsync(true);
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
    }
}