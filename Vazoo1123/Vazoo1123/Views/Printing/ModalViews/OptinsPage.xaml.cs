using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Vazoo1123.ViewModels.Printing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Printing.ModalViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OptinsPage : PopupPage
    {
        PrintingShipingLabeMW printingShipingLabe;
        Frame frame = null;
        StackLayout currentStackLayout = null;
        Button currentBtn = null;

        public OptinsPage (PrintingShipingLabeMW printingShipingLabeMW, bool isCountUSPS, bool isCountUPS, bool isCountFedEx)
		{
            printingShipingLabe = printingShipingLabeMW;
			InitializeComponent ();
            BindingContext = printingShipingLabe;
            currentStackLayout = stLaUSPS;
            btnUSPS.IsEnabled = isCountUSPS;
            btnUPS.IsEnabled = isCountUPS;
            btnFedEx.IsEnabled = isCountFedEx;

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (frame != ((Frame)sender))
            {
                if (frame != null)
                {
                    frame.BorderColor = Color.White;
                }
                frame = ((Frame)sender);
                string id = frame.FindByName<Label>("IdLabel").Text;
                if (printingShipingLabe.SetCarrier(id))
                {
                    frame.BorderColor = Color.FromHex("#2abdea");
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if(button.Text == "USPS" && currentStackLayout != stLaUSPS)
            {
                printingShipingLabe.TypeShipeMethod = "USPS";
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
            else if(button.Text == "UPS" && currentStackLayout != stLaUPS)
            {
                printingShipingLabe.TypeShipeMethod = "UPS";
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
            else if(button.Text == "FedEx" && currentStackLayout != stLaFedEx)
            {
                printingShipingLabe.TypeShipeMethod = "FedEx";
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