using System;
using System.Linq;
using Vazoo1123.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vazoo1123.Views.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        private RegistrationViewModels registrationViewModels = null;
        private bool isValidPsw = false;
        private bool isEqpsw = true;

        public Registration()
        {
            registrationViewModels = new RegistrationViewModels() { Navigation = this.Navigation};
            InitializeComponent();
            BindingContext = registrationViewModels;
        }

        private void OnImageNameTapped(object sender, EventArgs args)
        {
            if (psw.IsPassword)
            {
                psw.IsPassword = false;
                cpsw.IsPassword = false;
            }
            else
            {
                psw.IsPassword = true;
                cpsw.IsPassword = true;
            }
        }

        private void TextValid_Password(object sender, TextChangedEventArgs e)
        {
            char[] notValidChars = new char[]
            {'`','~','!','@','$','%','^','&','*','-','_','=','+','"','|',':','<','>',';', '#' };
            bool isSpecialChar = false;
            bool isNumber = false;
            bool isCount = false;
            bool isUpper = false;
            string password = psw.Text;
            foreach (var notValidChar in notValidChars)
            {
                if (password.Contains(notValidChar))
                {
                    isSpecialChar = true;
                    break;
                }
            }
            isNumber = password.Any(ch => char.IsNumber(ch));
            isUpper = password.Any(ch => char.IsUpper(ch));
            isCount = password.Count() >= 7 ? true : false;
            if (isSpecialChar && isNumber && isCount && isUpper)
            {
                psw.BackgroundColor = Color.FromRgba(0, 255, 0, 122);
                isValidPsw = true;
            }
            else
            {
                psw.BackgroundColor = Color.FromRgba(255, 0, 0, 122);
                isValidPsw = false;
            }
            CountValid_Password();
        }

        private void CountValid_Password()
        {
            if (psw.Text == cpsw.Text)
            {
                cpsw.BackgroundColor = Color.FromRgba(0, 255, 0, 122);
                isEqpsw = true;
            }
            else
            {
                cpsw.BackgroundColor = Color.FromRgba(255, 0, 0, 122);
                isEqpsw = false;
            }
            SetEnableVBtn();
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            image.HeightRequest = onePercentheigth * 25;
        }

        private void SetEnableVBtn()
        {
            if (isValidPsw && isEqpsw)
            {
                sigUpBtn.IsEnabled = true;
            }
            else
            {
                sigUpBtn.IsEnabled = false;
            }
        }
    }
}