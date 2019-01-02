using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vazoo1123.NewElement;
using Vazoo1123.ViewModels.Dashbord;
using Vazoo1123.Views.LoadViews;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltreModalPage : PopupPage
    {
        DashbordMW dashbordMW = null;

        public FiltreModalPage(DashbordMW dashbordMW)
        {
            this.dashbordMW = dashbordMW;
            InitializeComponent();
            BindingContext = dashbordMW;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private async void RadioButton_Clicked(object sender, EventArgs e)
        {
            dashbordMW.countPage = 0;
            var inp = ((CrossAddRadiobtn)sender);
            int idInp = inp.Idi;
            if (dashbordMW.Type != idInp)
            {
                await PopupNavigation.PopAsync(true);
                dashbordMW.Type = idInp;
                int stateAuth = 0;
                await Task.Run(async () =>
                {
                    stateAuth = await dashbordMW.GetOrder(true);
                });
                if (stateAuth == 3)
                {
                    dashbordMW.SelectProduct = new List<Models.OrderInfo>();
                    dashbordMW.CountSelectOrder = "";
                    if (dashbordMW.Type == 1)
                    {
                        dashbordMW.Title = $"Paid {dashbordMW.countOrder}";
                        dashbordMW.TypeCheck = true;
                        dashbordMW.TypeCheck1 = false;
                        dashbordMW.TypeCheck2 = false;
                        dashbordMW.TypeCheck3 = false;
                        dashbordMW.menuDetalePage.CheckAndSetCountDashbord(dashbordMW.countOrder);
                    }
                    else if (dashbordMW.Type == 2)
                    {
                        dashbordMW.Title = $"Sold Last 3 month {dashbordMW.countOrder}";
                        dashbordMW.TypeCheck = false;
                        dashbordMW.TypeCheck1 = true;
                        dashbordMW.TypeCheck2 = false;
                        dashbordMW.TypeCheck3 = false;
                    }
                    else if (dashbordMW.Type == 3)
                    {
                        dashbordMW.Title = $"Labels Printed Last 72h {dashbordMW.countOrder}";
                        dashbordMW.TypeCheck = false;
                        dashbordMW.TypeCheck1 = false;
                        dashbordMW.TypeCheck2 = true;
                        dashbordMW.TypeCheck3 = false;
                    }
                    else if (dashbordMW.Type == 4)
                    {
                        dashbordMW.Title = $"Printing label error {dashbordMW.countOrder}";
                        dashbordMW.TypeCheck = false;
                        dashbordMW.TypeCheck1 = false;
                        dashbordMW.TypeCheck2 = false;
                        dashbordMW.TypeCheck3 = true;
                    }
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
}