using Plugin.Settings;
using Prism.Mvvm;
using System.Threading.Tasks;
using Vazoo1123.Service;

namespace Vazoo1123.ViewModels
{
    public class MenuMW : BindableBase
    {
        public ManagerVazoo managerVazoo = null;
        public MenuMW()
        {
            managerVazoo = new ManagerVazoo();
            NameProfile = CrossSettings.Current.GetValueOrDefault("userName", "");
        }

        private string nameProfile;
        public string NameProfile
        {
            get { return nameProfile; }
            set
            {
                if(value != "")
                {
                    SetProperty(ref nameProfile, value);
                }
                else
                {
                    SetProperty(ref nameProfile, CrossSettings.Current.GetValueOrDefault("userName", "N/D"));
                }
            }
        }

        private int countMesage;
        public int CountMesage
        {
            get { return countMesage; }
            set
            {
                if (value != 0)
                {
                    SetProperty(ref countMesage, value);
                }
                else
                {
                    SetProperty(ref countMesage, 0);
                }
            }
        }

        private string countDashbord;
        public string CountDashbord
        {
            get { return countDashbord; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref countDashbord, value);
                }
                else
                {
                    SetProperty(ref countDashbord, "0");
                }
            }
        }

        public void CheckAndSetCountDashbord()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    if(managerVazoo.orderInfos != null && managerVazoo.orderInfos.Count != 0)
                    {
                        CountDashbord = managerVazoo.orderInfos.Count.ToString();
                        break;
                    }
                }
            });
        }
    }
}