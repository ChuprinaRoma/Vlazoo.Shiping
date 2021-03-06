﻿using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Vazoo1123.ViewModels.Dashbord;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptinsPage : PopupPage
    {
        FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV;
        Frame frame = null;
        StackLayout currentStackLayout = null;
        Button currentBtn = null;
        public OptinsPage(FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV, bool isCountUSPS, bool isCountUPS, bool isCountFedEx)
        {
            this.fullInfoOneOrderAndPrintingMV = fullInfoOneOrderAndPrintingMV;
            InitializeComponent();
            BindingContext = fullInfoOneOrderAndPrintingMV;
            currentStackLayout = stLaUSPS;
            btnUSPS.IsEnabled = isCountUSPS;
            btnUPS.IsEnabled = isCountUPS;
            btnFedEx.IsEnabled = isCountFedEx;
        }
        
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (frame != ((Frame)sender))
            {
                if (frame != null)
                {
                    frame.BorderColor = Color.White;
                }
                frame = ((Frame)sender);
                string id = frame.FindByName<Label>("IdLabel").Text;
                if (fullInfoOneOrderAndPrintingMV.SetCarrier(id))
                {
                    frame.BorderColor = Color.FromHex("#2c4dff");
                }
                await PopupNavigation.PopAsync(true);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if (button.Text == "USPS" && currentStackLayout != stLaUSPS)
            {
                fullInfoOneOrderAndPrintingMV.TypeShipeMethod = "USPS";
                await stLaUSPS.TranslateTo(1000, 0, 0);
                await currentStackLayout.FadeTo(0, 100);
                currentStackLayout.IsVisible = false;
                stLaUSPS.IsVisible = true;
                await Task.WhenAll(
                    stLaUSPS.TranslateTo(0, 0, 350)
                );
                currentStackLayout.IsVisible = false;
                await currentStackLayout.FadeTo(1, 0);
                currentStackLayout = stLaUSPS;
                currentBtn = button;
            }
            else if (button.Text == "UPS" && currentStackLayout != stLaUPS)
            {
                fullInfoOneOrderAndPrintingMV.TypeShipeMethod = "UPS";
                if (currentStackLayout == stLaUSPS)
                {
                    await stLaUPS.TranslateTo(-1000, 0, 0);
                    await currentStackLayout.FadeTo(0, 100);
                    currentStackLayout.IsVisible = false;
                    stLaUPS.IsVisible = true;
                    await Task.WhenAll(
                        stLaUPS.TranslateTo(0, 0, 350)
                    );
                }
                else
                {
                    await stLaUPS.TranslateTo(1000, 0, 0);
                    await currentStackLayout.FadeTo(0, 100);
                    currentStackLayout.IsVisible = false;
                    stLaUPS.IsVisible = true;
                    await Task.WhenAll(
                        stLaUPS.TranslateTo(0, 0, 350)
                    );
                }
                currentStackLayout.IsVisible = false;
                await currentStackLayout.FadeTo(1, 0);
                currentStackLayout = stLaUPS;
            }
            else if (button.Text == "FedEx" && currentStackLayout != stLaFedEx)
            {
                fullInfoOneOrderAndPrintingMV.TypeShipeMethod = "FedEx";
                await stLaFedEx.TranslateTo(-1000, 0, 0);
                await currentStackLayout.FadeTo(0, 100);
                currentStackLayout.IsVisible = false;
                stLaFedEx.IsVisible = true;
                await Task.WhenAll(
                    stLaFedEx.TranslateTo(0, 0, 350)
                );
                currentStackLayout.IsVisible = false;
                await currentStackLayout.FadeTo(1, 0);
                currentStackLayout = stLaFedEx;
            }
        }
    }
}