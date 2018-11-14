using Rg.Plugins.Popup.Pages;
using System;
using Vazoo1123.NewElement;
using Vazoo1123.ViewModels.Mesages;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModelFilterMesage : PopupPage
    {
        private MesagesFolderMV mesagesFolderMV = null;
        private CrossAddRadiobtn crossAddRadiobtnSelect = null;
        public ModelFilterMesage (MesagesFolderMV mesagesFolderMV)
		{
            this.mesagesFolderMV = mesagesFolderMV;
            InitializeComponent ();
		}

        private void CrossAddRadiobtn_Clicked(object sender, EventArgs e)
        {
            crossAddRadiobtnSelect = ((CrossAddRadiobtn)sender);
        }
    }
}