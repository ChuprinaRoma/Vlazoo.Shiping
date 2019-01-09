using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.Views.PageApp.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.ModalView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirm : PopupPage
    {
        private ICreateShiping createShiping = null;
        private List<Carrier> carriers = null;
        private bool isPrinted = true;
        private INavigation navigation = null;

        public Confirm(ICreateShiping createShiping, string postageCarr, double postageBalance, INavigation navigation, bool isPrinted = true, List<Carrier> carriers = null, Carrier carrier = null)
        {
            this.navigation = navigation;
            this.isPrinted = isPrinted;
            this.createShiping = createShiping;
            InitializeComponent();;
            this.carriers = new List<Carrier>();
            if (carriers == null)
            {
                this.carriers.Add(carrier);
            }
            else
            {
                this.carriers.AddRange(carriers);
            }
            lPostage.Text = postageCarr;
            lCount.Text = this.carriers.Count.ToString();
            BindingContext = this.carriers;
            this.postageBalance.Text = $"${postageBalance}";
            EqualsBalanceAndSetBalanse(Convert.ToDouble(postageCarr), postageBalance);
            Init(postageCarr, postageBalance);
        }

        private void Init(string postageCarr, double postageBalance)
        {
            double postageCarri = Convert.ToDouble(postageCarr);
            lp.Text = postageCarri > postageBalance ? (postageCarri - postageBalance).ToString() : "0";
        }

        private async void EqualsBalanceAndSetBalanse(double postageCarr, double postageBalance)
        {
            if(postageBalance >= postageCarr)
            {
                this.postageBalance.TextColor = Color.FromHex("#088A4B");
                btnConfirm.IsEnabled = true;
            }
            else
            {
                this.postageBalance.TextColor = Color.FromHex("#FF0040");
                btnConfirm.IsEnabled = false;
            }
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (isPrinted)
            {
                createShiping.ShippingCreate();
            }
            else
            {
                createShiping.ShippingCreate1();
            }
        }

        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
        
        public string[] NameDropDwnSourse { get; set; }
        
        public string SelectDropDwmSourse { get; set; }

        public List<string[]> DropDwnChooseRemovePrinters { get; set; }

        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            if (e.NewItemIndex != -1 && e.NewItem != null && e.NewItem.ToString() != "")
            {

            }
        }

        private int InitDropDwnChoseRemotePrinter()
        {
            List<string[]> dropDwnChooseRemovePrinters = null;
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 3; //ManagerVazoo.PrintingWork("OptionsGet", ref dropDwnChooseRemovePrinters, idCompany, email, psw);
            Task.Run(() =>
            {
                DropDwnChooseRemovePrinters = dropDwnChooseRemovePrinters;
                SelectDropDwmSourse = CrossSettings.Current.GetValueOrDefault("printer", "Default Printer,\"\"");
                if (stateAuth == 3)
                {
                    NameDropDwnSourse = dropDwnChooseRemovePrinters.Select(d => d[0]).ToArray();
                }
            });
            return stateAuth;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            await navigation.PushAsync(new Replenishment($"https://vlazoo.com/BuyPostageCC.aspx?ClientID={idCompany}&Amount={lp.Text}", "Visa or MasterCard"));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            await navigation.PushAsync(new Replenishment($"https://vlazoo.com/BuyPostagePP.aspx?ClientID={idCompany}&Amount={lp.Text}", "PayPal"));
        }
    }
}