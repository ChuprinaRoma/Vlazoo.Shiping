using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vazoo1123.Service;
using Vazoo1123.Views.LoadViews;
using Vazoo1123.Views.PageApp.Profile;
using Xamarin.Forms;

namespace Vazoo1123.ViewModels.Profile
{
    public class ProfileMW : BindableBase
    {
        private ManagerVazoo managerVazoo = null;
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand ToEditCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand EBayAllowAccessCommand { get; set; } 
        public ProfileData Profile { get; set; }
        public INavigation Navigation { get; set; }

        public ProfileMW(ManagerVazoo managerVazoo)
        {
            this.managerVazoo = managerVazoo;
            Profile = new ProfileData();
            UpdateCommand = new DelegateCommand(Update);
            ToEditCommand = new DelegateCommand(ToEdit);
            EditCommand = new DelegateCommand(Edit);
            EBayAllowAccessCommand = new DelegateCommand(EBayAllowAccess);
            Init();
        }

        private string idCompany;
        public string IdCompany
        {
            get { return idCompany; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref idCompany, value);
                }
                else
                {
                    SetProperty(ref idCompany , "- - - - - - - -");
                }
            }
        }

        private string commpanyName;
        public string CommpanyName
        {
            get { return commpanyName; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref commpanyName, value);
                }
                else
                {
                    SetProperty(ref commpanyName, "- - - - - - - -");
                }
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref userName, value);
                }
                else
                {
                    SetProperty(ref userName, "- - - - - - - -");
                }
            }
        }

        private string client;
        public string Client
        {
            get { return client; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref client, value);
                }
                else
                {
                    SetProperty(ref client, "- - - - - - - -");
                }
            }
        }

        private string contactName;
        public string ContactName
        {
            get { return contactName; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref contactName, value);
                    Profile.ContactName = value;
                }
                else
                {
                    SetProperty(ref contactName, "- - - - - - - -");
                }
            }
        }

        private string contactEmail;
        public string ContactEmail
        {
            get { return contactEmail; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref contactEmail, value);
                    Profile.ClientEmail = value;
                }
                else
                {
                    SetProperty(ref contactEmail, "- - - - - - - -");
                }
            }
        }

        private string contactPhone;
        public string ContactPhone
        {
            get { return contactPhone; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref contactPhone, value);
                    Profile.ClientPhone = value;
                }
                else
                {
                    SetProperty(ref contactPhone, "- - - - - - - -");
                }
            }
        }

        private string contactName1;
        public string ContactName1
        {
            get { return contactName1; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref contactName1, value);
                    Profile.WarehouseContactName = value;
                }
                else
                {
                    SetProperty(ref contactName1, "- - - - - - - -");
                }
            }
        }

        private string company;
        public string Company
        {
            get { return company; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref company, value);
                    Profile.LegalName = value;
                }
                else
                {
                    SetProperty(ref company, "- - - - - - - -");
                }
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref address, value);
                    Profile.WarehouseAddress1 = value.Remove(value.IndexOf(','));
                    Profile.WarehouseAddress2 = value.Remove(0, value.IndexOf(',')+2);
                }
                else
                {
                    SetProperty(ref address, "- - - - - - - -");
                }
            }
        }

        private string floor;
        public string Floor
        {
            get { return floor; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref floor, value);
                    Profile.WarehouseFloor = value;
                }
                else
                {
                    SetProperty(ref floor, "- - - - - - - -");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref city, value);
                    Profile.WarehouseCity = value;
                }
                else
                {
                    SetProperty(ref city, "- - - - - - - -");
                }
            }
        }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref state, value);
                    Profile.WarehouseState = value;
                }
                else
                {
                    SetProperty(ref state, "- - - - - - - -");
                }
            }
        }

        private string zip;
        public string Zip
        {
            get { return zip; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref zip, value);
                    Profile.WarehouseZip = value;
                }
                else
                {
                    SetProperty(ref zip, "- - - - - - - -");
                }
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != "")
                {
                    SetProperty(ref phone, value);
                    Profile.WarehousePhone = value;
                }
                else
                {
                    SetProperty(ref phone, "- - - - - - - -");
                }
            }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set
            {
                    SetProperty(ref userID, value);
                    Profile.EBayUserID = value;
            }
        }

        private string SesionID { get; set; }

        private string[] nameDropDwnSourse = null;
        public string[] NameDropDwnSourse
        {
            get => nameDropDwnSourse;
            set => SetProperty(ref nameDropDwnSourse, value);
        }

        private string selectDropDwmSourse = "";
        public string SelectDropDwmSourse
        {
            get => selectDropDwmSourse;
            set => SetProperty(ref selectDropDwmSourse, value);
        }

        public List<string[]> DropDwnChooseRemovePrinters { get; set; }

        private async void Init()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            if(stateAuth == 3)
            {
                stateAuth = InitDropDwnChoseRemotePrinter();
            }
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 3)
            {
                IdCompany = _xzType[1];
                CommpanyName = _xzType[2];
                UserName = _xzType[3];
                Client = _xzType[2];
                ContactName = _xzType[5];
                ContactEmail = _xzType[6];
                ContactPhone = _xzType[7];
                ContactName1 = _xzType[8];
                Company = _xzType[9];
                Address = _xzType[10];
                Floor = _xzType[11];
                City = _xzType[12];
                State = _xzType[13];
                Zip = _xzType[14];
                Phone = _xzType[15];
                UserID = _xzType[16];
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("Error"), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
        }

        private int InitDropDwnChoseRemotePrinter()
        {
            List<string[]> dropDwnChooseRemovePrinters = null;
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            int stateAuth = managerVazoo.PrintingWork("OptionsGet", ref dropDwnChooseRemovePrinters, idCompany, email, psw);
            Task.Run(() =>
            {
                DropDwnChooseRemovePrinters = dropDwnChooseRemovePrinters;
                SelectDropDwmSourse = CrossSettings.Current.GetValueOrDefault("printer", "");
                if (stateAuth == 3)
                {
                    NameDropDwnSourse = dropDwnChooseRemovePrinters.Select(d => d[0]).ToArray();
                }
            });
            return stateAuth;
        }

        private async void Update()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("profileGet", ref description, null, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            await PopupNavigation.PopAllAsync();
           
            if (stateAuth == 3)
            {
                IdCompany = _xzType[1];
                CommpanyName = _xzType[2];
                UserName = _xzType[3];
                Client = _xzType[2];
                ContactName = _xzType[5];
                ContactEmail = _xzType[6];
                ContactPhone = _xzType[7];
                ContactName1 = _xzType[8];
                Company = _xzType[9];
                Address = _xzType[10];
                Floor = _xzType[11];
                City = _xzType[12];
                State = _xzType[13];
                Zip = _xzType[14];
                Phone = _xzType[15];
                UserID = _xzType[16];
            }
            else if (stateAuth == 2)
            {

                await PopupNavigation.PushAsync(new Error("can not update, try again later"), true);
                
            }
            else if (stateAuth == 1)
            {
                    await PopupNavigation.PushAsync(new Error("No network"), true);
            }
            else if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
        }
        
        private async void ToEdit()
        {
            await PopupNavigation.PushAsync(new EditProfileMoldal(this), true);
        }

        private async void Edit()
        {
            string description = null;
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("ProfileSet", ref description, Profile, idCompany, email, psw);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            Update();
            if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error("could not save data"), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
        }

        private async void EBayAllowAccess()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("EBayAllowAccess", ref description, null, idCompany, email, psw, UserID);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
            else if(stateAuth == 3)
            {
                await Navigation.PushAsync(new PageAvthorEbay(_xzType[1], this));
                SesionID = _xzType[2];
            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
        }

        public async void EBayConfirm()
        {
            string description = null;
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string email = CrossSettings.Current.GetValueOrDefault("userName", "");
            string idCompany = CrossSettings.Current.GetValueOrDefault("idCompany", "");
            string psw = CrossSettings.Current.GetValueOrDefault("psw", "");
            string[] _xzType = managerVazoo.PofiletWork("EBayConfirm", ref description, null, idCompany, email, psw, SesionID);
            int stateAuth = Convert.ToInt32(_xzType[0]);
            await PopupNavigation.PopAllAsync();
            if (stateAuth == 4)
            {
                await PopupNavigation.PushAsync(new Error("Technical works on the server"), true);
            }
            else if (stateAuth == 3)
            {

            }
            else if (stateAuth == 2)
            {
                await PopupNavigation.PushAsync(new Error(description), true);
            }
            else if (stateAuth == 1)
            {
                await PopupNavigation.PushAsync(new Error("No network"), true);
            }
        }
    }
}