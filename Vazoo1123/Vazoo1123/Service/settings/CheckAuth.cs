using Plugin.Settings;
using Vazoo1123.Views.A_R;
using Vazoo1123.Views.Menu;
using Xamarin.Forms;

namespace Vazoo1123.Service.settings
{
    class CheckAuth
    {
        public static void SavingAccount(string idCompany, string userName, string psw)
        {
            CrossSettings.Current.AddOrUpdateValue("idCompany", idCompany);
            CrossSettings.Current.AddOrUpdateValue("userName", userName);
            CrossSettings.Current.AddOrUpdateValue("psw", psw);
        }

        public static void RmovegAccount()
        {
            CrossSettings.Current.Remove("idCompany");
            CrossSettings.Current.Remove("userName");
            CrossSettings.Current.Remove("psw");
            CrossSettings.Current.Remove("printer");
        }

        public void Authorization()
        {
            string userName = CrossSettings.Current.GetValueOrDefault("userName", "");
            if (userName != "")
            {
                string description = null;
                Application.Current.MainPage = new MenuDetalePage();
                
            }
            else
            {
                Application.Current.MainPage = new FirstPage();
            }
        }
    }
}