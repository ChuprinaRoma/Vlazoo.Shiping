using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.Models;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.PageApp.Dashbord;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels.Dashbord
{
    public class DashbordMW : BindableBase
    {
        public ManagerVazoo managerVazoo = null;
        public INavigation Navigation { get; set; }
        public DelegateCommand ToBulkPostagePrintingCommand { get; set; }

        public DashbordMW(ManagerVazoo managerVazoo)
        {
            this.managerVazoo = managerVazoo;
            SelectProduct = new List<OrderInfo>();
            serchOrder = new List<OrderInfo>();
            Type = 1;
            Init();
            ToBulkPostagePrintingCommand = new DelegateCommand(ToBulkPostagePrinting);
        }
        
        private List<OrderInfo> serchOrder = null;
        public List<OrderInfo> SerchOrder
        {
            get { return serchOrder; }
            set { SetProperty(ref serchOrder, value); }
        }

        private List<OrderInfo> selectProduct = null;
        public List<OrderInfo> SelectProduct
        {
            get { return selectProduct; }
            set { SetProperty(ref selectProduct, value); }
        }

        private ObservableCollection<OrderInfo> product = null;
        public ObservableCollection<OrderInfo> Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }
        ////////////////////////////////////////////////////
        private int type = 0;
        public int Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }
        ///////////////////////////////////////////////////
        private bool typeCheck;
        public bool TypeCheck
        {
            get { return typeCheck; }
            set { SetProperty(ref typeCheck, value); }
        }

        private bool typeCheck1;
        public bool TypeCheck1
        {
            get { return typeCheck1; }
            set { SetProperty(ref typeCheck1, value); }
        }

        private bool typeCheck2;
        public bool TypeCheck2
        {
            get { return typeCheck2; }
            set { SetProperty(ref typeCheck2, value); }
        }

        private string title = "";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public int countPage = 0;
        public int countFullPage = 0;

        public async void Init()
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.DashbordWork("OrdersGet", ref description, Type, ref countPage, idCompany, email, psw);
            });
            if (stateAuth == 3)
            {
                Product = new ObservableCollection<OrderInfo>(managerVazoo.orderInfos.GetRange(0, managerVazoo.orderInfos.Count >= 40 ? 40 : managerVazoo.orderInfos.Count));
                Title = $"Awaining {Product.Count.ToString()}";
                TypeCheck = true;
                TypeCheck1 = false;
                TypeCheck2 = false;
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
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

        public async Task<int> GetOrder(bool isLoad)
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = 0;
            await Task.Run(() =>
            {
                stateAuth = managerVazoo.DashbordWork("OrdersGet", ref description, Type, ref countPage, idCompany, email, psw);
            });
            if (stateAuth == 3 && isLoad)
            {
                Product = new ObservableCollection<OrderInfo>(managerVazoo.orderInfos
                    .GetRange(0, managerVazoo.orderInfos.Count >= 40 ? 40 : managerVazoo.orderInfos.Count));
            }
            return stateAuth;
        }

        private async void ToBulkPostagePrinting()
        {
            if (SelectProduct.Count == 1)
            {
                await Navigation.PushAsync(new OrderOnePinting(SelectProduct[0], managerVazoo));
            }
            else if(SelectProduct.Count != 0)
            {
                await Navigation.PushAsync(new BulkPostagePrinting(managerVazoo, SelectProduct));
            }
            else
            {
                await PopupNavigation.PushAsync(new Error("To print a label, select the product for which you want to print"), true);
            }
        }
        
        public void SerchWork(string serchText)
        {
            //List<OrderInfo> serchOrderInfo = new List<OrderInfo>();
            //if (serchText != "")
            //{
            //    serchOrderInfo.AddRange(Product.FindAll(p => !serchOrderInfo.Contains(p) && p.EBayItemID.Length > serchText.Length && p.EBayItemID.Remove(serchText.Length) == serchText));
            //    serchOrderInfo.AddRange(Product.FindAll(p => !serchOrderInfo.Contains(p) && p.EBayUserID.Length > serchText.Length && p.EBayUserID.Remove(serchText.Length) == serchText));
            //    serchOrderInfo.AddRange(Product.FindAll(p => !serchOrderInfo.Contains(p) && p.RecordNumber.Length > serchText.Length && p.RecordNumber.Remove(serchText.Length) == serchText));
            //    serchOrderInfo.AddRange(Product.FindAll(p => !serchOrderInfo.Contains(p) && p.ItemTitle.Length > serchText.Length && p.ItemTitle.Remove(serchText.Length) == serchText));
            //    serchOrderInfo.AddRange(Product.FindAll(p => !serchOrderInfo.Contains(p) && p.ShopperEmail.Length > serchText.Length && p.ShopperEmail.Remove(serchText.Length) == serchText));
            //    SerchOrder = serchOrderInfo;
            //}
            //else
            //{
            //    SerchOrder = Product;
            //}
        }
    }
}