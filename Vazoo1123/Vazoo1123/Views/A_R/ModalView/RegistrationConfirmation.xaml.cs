using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.A_R.ModalView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationConfirmation : PopupPage
    {
		public RegistrationConfirmation ()
		{
			InitializeComponent ();
            BindingContext = new RegistrationConfirmationMW();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}