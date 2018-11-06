using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels.Printing;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Printing.ModalViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShipToAddress : PopupPage
    {
		public ShipToAddress (PrintingShipingLabeMW printingShipingLabeMW)
		{
			InitializeComponent ();
            BindingContext = printingShipingLabeMW;
		}
	}
}