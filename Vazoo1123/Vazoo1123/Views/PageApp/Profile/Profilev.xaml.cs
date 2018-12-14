using Plugin.InputKit.Shared.Utils;
using Plugin.Settings;
using System;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileV : ContentPage
	{
        public ProfileMW profileMW = null;

        public ProfileV(ManagerVazoo managerVazoo)
        {
            InitializeComponent();
            profileMW = new ProfileMW(managerVazoo)
            { Navigation = this.Navigation};
            BindingContext = profileMW;
        }

        private void OnOrintable(object s, EventArgs e)
        {
            if(this.Height > this.Width)
            {
                scrol.Orientation = ScrollOrientation.Vertical;
                body.Orientation = StackOrientation.Vertical;
            }
            else
            {
                scrol.Orientation = ScrollOrientation.Horizontal;
                body.Orientation = StackOrientation.Horizontal;
            }
        }

        private void Dropdown_SelectedItemChanged(object sender, SelectedItemChangedArgs e)
        {
            if (e.NewItemIndex != -1 && e.NewItem != null && e.NewItem.ToString() != "")
            {
                CrossSettings.Current.AddOrUpdateValue("printer", $"{profileMW.DropDwnChooseRemovePrinters[e.NewItemIndex][0]},{profileMW.DropDwnChooseRemovePrinters[e.NewItemIndex][1]}");
            }
        }
    }
}