using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels.Dashbord;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Dashbord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditShipToAddres : PopupPage
    {
        private FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV = null;
        
        public EditShipToAddres (FullInfoOneOrderAndPrintingMV fullInfoOneOrderAndPrintingMV)
		{
            this.fullInfoOneOrderAndPrintingMV = fullInfoOneOrderAndPrintingMV;
			InitializeComponent ();
            BindingContext = fullInfoOneOrderAndPrintingMV;
        }
	}
}