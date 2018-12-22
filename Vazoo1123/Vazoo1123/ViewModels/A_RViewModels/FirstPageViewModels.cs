using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System;
using Vazoo1123.Views;
using Vazoo1123.Views.A_R;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels.A_RViewModels
{
    class FirstPageViewModels
    {
        public INavigation Navigation { get; set; }
        public DelegateCommand ToHelpCommand { get; set; }
        public DelegateCommand  ToSigUpCommand { get; set; }
        public DelegateCommand  ToLogInCommand { get; set; }
        public DelegateCommand ToYoutubeCommand { get; set; }
        public DelegateCommand ToFaceBockCommand { get; set; }

        public FirstPageViewModels()
        {
            ToHelpCommand = new DelegateCommand(ToHelp);
            ToLogInCommand = new DelegateCommand(ToLogIn);
            ToSigUpCommand = new DelegateCommand(ToSigUp);
            ToYoutubeCommand = new DelegateCommand(ToYoutube);
            ToFaceBockCommand = new DelegateCommand(ToFaceBock);
        }

        private async void ToHelp()
        {
            await PopupNavigation.PushAsync(new ContexWindowHelp(), true);
        }

        private void ToSigUp()
        {
            Navigation.PushModalAsync(new Registration());
        }

        private void ToLogIn()
        {
            Navigation.PushModalAsync(new Authorization());
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