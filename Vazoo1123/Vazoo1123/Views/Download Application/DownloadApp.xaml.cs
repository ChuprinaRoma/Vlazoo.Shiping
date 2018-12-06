using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Download_Application
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DownloadApp : ContentPage
	{
		public DownloadApp ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://vlazoo.com/Attachments/PrintLabels.zip"));
        }
    }
}