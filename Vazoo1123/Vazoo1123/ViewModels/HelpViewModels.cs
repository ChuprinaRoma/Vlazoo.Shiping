﻿using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels
{
    class HelpViewModels : BindableBase
    {
        public DelegateCommand ToBackCommand { get; set; }
        public INavigation Navigation { get; set; }
        public DelegateCommand ToVlazoo_WebCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        private ManagerVazoo managerVazoo = null;
        public DelegateCommand ToYoutubeCommand { get; set; }
        public DelegateCommand ToFaceBockCommand { get; set; }

        public HelpViewModels()
        {
            managerVazoo = new ManagerVazoo();
            ToBackCommand = new DelegateCommand(ToBack);
            ToVlazoo_WebCommand = new DelegateCommand(ToVlazoo_Web);
            SendCommand = new DelegateCommand(Send);
            ToYoutubeCommand = new DelegateCommand(ToYoutube);
            ToFaceBockCommand = new DelegateCommand(ToFaceBock);
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string mesage;
        public string Mesage
        {
            get { return mesage; }
            set { SetProperty(ref mesage, value); }
        }

        private void ToBack()
        {
            Navigation.PopModalAsync();
        }

        private void ToVlazoo_Web()
        {
            Device.OpenUri(new Uri("https://vlazoo.com"));
        }

        private async void Send()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage(), true);
            int stateAuth = managerVazoo.SuportWork("help", ref description, Email, Name,  Mesage);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                await PopupNavigation.PushAsync(new Compleat(""));
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
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