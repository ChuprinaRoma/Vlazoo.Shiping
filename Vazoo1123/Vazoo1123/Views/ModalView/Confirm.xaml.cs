using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using Vazoo1123.Models;
using Vazoo1123.Service;
using Vazoo1123.ViewModels.Printing;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.ModalView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirm : PopupPage
    {
        private ICreateShiping createShiping = null;
        private List<Carrier> carriers = null;
        public Confirm(ICreateShiping createShiping, string postageCarr, List<Carrier> carriers = null, Carrier carrier = null)
        {
            this.createShiping = createShiping;
            InitializeComponent();;
            this.carriers = new List<Carrier>();
            if (carriers == null)
            {
                this.carriers.Add(carrier);
            }
            else
            {
                this.carriers.AddRange(carriers);
            }
            lPostage.Text = postageCarr;
            lCount.Text = this.carriers.Count.ToString();
            BindingContext = this.carriers;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            createShiping.ShippingCreate();
        }

        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}