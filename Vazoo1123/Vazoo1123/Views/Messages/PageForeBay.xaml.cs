using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Messages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageForeBay : ContentPage
	{
        private string eBayUrl = null;

		public PageForeBay (string eBayUrl, string subject)
		{
            this.eBayUrl = eBayUrl;
			InitializeComponent ();
            subj.Text = subject;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(eBayUrl));
        }
    }
}