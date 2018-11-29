using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
            gr.SelectedIndex = this.mesagesFolderMV.Type - 1;
        }

        private async void CrossAddRadiobtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAllAsync(true);
            crossAddRadiobtnSelect = ((CrossAddRadiobtn)sender);
            mesagesFolderMV.Type = crossAddRadiobtnSelect.Idi;
            mesagesFolderMV.Name = crossAddRadiobtnSelect.Text;
            mesagesFolderMV.InitMessages();
        }
    }
}