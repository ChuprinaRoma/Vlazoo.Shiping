using System;
using System.Linq;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.Models;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.PageApp.Dashbord;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Vazoo1123.Views.Menu;
using Vazoo1123.Views.Printing.ModalViews;
using Vazoo1123.Views.Messages;

namespace Vazoo1123.Views.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashbordMW dashbordMW = null;
        private ManagerVazoo managerVazoo = null;

        public DashboardPage(ManagerVazoo managerVazoo, MenuDetalePage menuDetalePage)
        {
            InitializeComponent();
            this.managerVazoo = managerVazoo;
            dashbordMW = new DashbordMW(managerVazoo, menuDetalePage)
            { Navigation = this.Navigation };
            InitElement();
            BindingContext = dashbordMW;
        }
        
        private async void InitElement()
        {
            await serchBar.TranslateTo(0, -100, 0);
        }

        private async void OnDoubleTapped(object sender, EventArgs e)
        {
            var elStaL = ((Frame)sender);
            string itemId = elStaL.FindByName<Label>("itemId").Text;
            string recordId = elStaL.FindByName<Label>("recordId").Text;
            if (dashbordMW.SelectProduct.FirstOrDefault(sp => sp.EBayItemID == itemId && sp.RecordNumber == recordId) != null)
            {
                elStaL.BorderColor = Color.FromHex("#bfebf9");
                await elStaL.ScaleTo(1, 100);
                dashbordMW.SelectProduct.Remove(dashbordMW.SelectProduct.FirstOrDefault(sp => sp.EBayItemID == itemId && sp.RecordNumber == recordId));
                if (dashbordMW.SelectProduct.Count == 0)
                {
                    dashbordMW.CountSelectOrder = "";
                    printing.Icon = "Printing.png";
                }
                else
                {
                    dashbordMW.CountSelectOrder = dashbordMW.SelectProduct.Count.ToString();
                }
            }
            else
            {
                elStaL.BorderColor = Color.FromHex("#2c4dff");
                printing.Icon = "Printing1.png;";
                await elStaL.ScaleTo(0.97, 50);
                dashbordMW.SelectProduct.Add(dashbordMW.Product.FirstOrDefault(sp => sp.EBayItemID == itemId && sp.RecordNumber == recordId));
                dashbordMW.CountSelectOrder = dashbordMW.SelectProduct.Count.ToString();
            }
        }
        
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (serchBar.IsVisible)
            {
                Rectangle rectangle = new Rectangle(0.5, 1, 1, bodySt.Bounds.Height + 60);
                AbsoluteLayout.SetLayoutBounds(bodySt, rectangle);
                await serchBar.TranslateTo(0, -100, 300);
                serchBar.IsVisible = false;
                OrderList.SetBinding(ListView.ItemsSourceProperty, new Binding("Product"));
            }
            else
            {
                serchBar.IsVisible = true;
                Rectangle rectangle = new Rectangle(0.5, 1, 1, bodySt.Bounds.Height - 60);
                AbsoluteLayout.SetLayoutBounds(bodySt, rectangle);
                await serchBar.TranslateTo(0, 0, 300);
                if (dashbordMW.SerchOrder.Count == 0)
                {
                    //dashbordMW.SerchOrder = dashbordMW.Product;
                }
                OrderList.SetBinding(ListView.ItemsSourceProperty, new Binding("SerchOrder"));
            }
        }

        private void body_SizeChanged(object sender, EventArgs e)
        {
            Rectangle rectangle = new Rectangle(0.5, 1, 1, body.Bounds.Height);
            AbsoluteLayout.SetLayoutBounds(bodySt, rectangle);
        }

        private void CrossEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry)sender);
            dashbordMW.SerchWork(entry.Text);
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new FiltreModalPage(dashbordMW), true);
        }

        private void OrderList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            bool isLoad = true;
             Task.Run( async() =>
            {
                if ((((OrderInfo)e.Item).EBayUserID == dashbordMW.Product[dashbordMW.Product.Count - 21].EBayUserID
                || ((OrderInfo)e.Item).EBayUserID == dashbordMW.Product[dashbordMW.Product.Count - 11].EBayUserID)
                && dashbordMW.Product.Count >= 40)
                {
                    if (dashbordMW.managerVazoo.orderInfos.Count-40 <= dashbordMW.Product.Count)
                    {
                        await Task.Run(() => dashbordMW.GetOrder(false));
                    }
                    Device.StartTimer(TimeSpan.FromSeconds(1),() =>
                    {
                        int countPageAddP = (dashbordMW.Product.Count / 10);
                        int pageOrMV = dashbordMW.managerVazoo.orderInfos.Count / 10;
                        int pageP = dashbordMW.Product.Count / 10;
                        int remainderOrMV = pageOrMV == pageP ? dashbordMW.managerVazoo.orderInfos.Count % 10 : 0;
                        if (dashbordMW.managerVazoo.orderInfos.Count != dashbordMW.Product.Count)
                        {
                            for (int i = (countPageAddP * 10); i < (countPageAddP * 10 + (remainderOrMV == 0 ? 10 : remainderOrMV)); i++)
                            {
                                dashbordMW.Product.Add(dashbordMW.managerVazoo.orderInfos[i]);
                            }
                        }
                        return false;
                    });
                }
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != "")
            {
                await PopupNavigation.PushAsync(new LabalPageView(label.Text));
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            string messageID = image.FindByName<Label>("MessageID").Text;
            await Navigation.PushAsync(new Conversation(managerVazoo, null, null, messageID));
        }
    }
}