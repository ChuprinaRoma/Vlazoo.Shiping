using Rg.Plugins.Popup.Pages;
using Vazoo1123.ViewModels.Profile;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfileMoldal : PopupPage
    {
		public EditProfileMoldal (ProfileMW profileMW)
		{
			InitializeComponent ();
            BindingContext = profileMW; 
		}
    }
}