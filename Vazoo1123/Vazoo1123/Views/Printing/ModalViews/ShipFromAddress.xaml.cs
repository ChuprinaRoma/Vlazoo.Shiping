using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels.Printing;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Printing.ModalViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShipFromAddress : PopupPage
    {
		public ShipFromAddress (PrintingShipingLabeMW printingShipingLabeMW)
		{
			InitializeComponent ();
            BindingContext = printingShipingLabeMW;
		}
	}
}