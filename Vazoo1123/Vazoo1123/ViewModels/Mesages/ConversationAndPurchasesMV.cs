using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.Messages;

namespace Vazoo1123.ViewModels.Mesages
{
    public class ConversationAndPurchasesMV : BindableBase
    {
        public ManagerVazoo managerVazoo = null;
        public DelegateCommand ToPurchasesCommand { get; set; }

        public ConversationAndPurchasesMV(ManagerVazoo managerVazoo, Models.Messages messages)
        {
            this.managerVazoo = managerVazoo;
            Messages = messages;
            ToPurchasesCommand = new DelegateCommand(ToPurchases);
            InitConversation();
            InitPurchases();
        }

        private Models.Messages messages = null;
        public Models.Messages Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
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

        private async void InitPurchases()
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
                stateAuth = managerVazoo.MesagesWork("Purchases", ref totalResulte, ref orderInfo, email, idCompany, psw, Messages.ID.ToString(), "0");
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
        }

        private async void InitConversation()
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
                stateAuth = managerVazoo.MesagesWork("Conversation", ref totalResulte, ref orderInfo, email, idCompany, psw, Messages.ID.ToString(), "0");
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
        }

        private async void ToPurchases()
        {
            if(OrderInfo != null)
            {
                await PopupNavigation.PushAsync(new Purchases(this));
            }
            else
            {
                await PopupNavigation.PushAsync(new Error("there is no order or it has not loaded yet"), true);
            }
        }
    }
}