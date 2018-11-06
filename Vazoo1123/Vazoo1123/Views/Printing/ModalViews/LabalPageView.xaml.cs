using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.Printing.ModalViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LabalPageView : PopupPage
    {
        string[] trackinImgs = null;
		public LabalPageView (string tracking)
		{
			InitializeComponent ();
            Init(tracking);
        }

        private void Init(string tracking)
        {
            trackinImgs = tracking.Split(',');
            if (trackinImgs.Length > 1)
            {
                for (int i = 0; i < trackinImgs.Length; i++)
                {
                    Button button = new Button();
                    button.Text = $"{i+1}";
                    button.BackgroundColor = Color.White;
                    button.TextColor = Color.FromHex("#2abdea");
                    button.Clicked += Button_Clicked;
                    btnTab.Children.Add(button);
                }
            }
            labelImj.Source = $"https://vlazoo.com/shippinglabels/{trackinImgs[0].Trim()}.png";
        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((StackLayout)sender);
            labelImj.HeightRequest = (Application.Current.MainPage.Height / 100) * 80;
            labelImj.WidthRequest = (stackLayout.Width / 100) * 99;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            labelImj.Source = $"https://vlazoo.com/shippinglabels/{trackinImgs[Convert.ToInt32(button.Text) - 1].Trim()}.png";
        }
    }
}