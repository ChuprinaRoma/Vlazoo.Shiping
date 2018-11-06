using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContexWindowHelp : PopupPage
    {
		public ContexWindowHelp ()
		{
			InitializeComponent ();
		}

        private async void Close(object s, EventArgs e)
        {
            await PopupNavigation.PopAllAsync();
        }

        private async void ToHelpe(object s, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
            await Navigation.PushModalAsync(new Helpe());
        }
    }
}