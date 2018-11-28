
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using Vazoo1123.Models;
using Vazoo1123.ViewModels.Printing.Models;
using Vazoo1123.ViewModels.Profile;

namespace Vazoo1123.Service
{
    public class ManagerVazoo
    {
        private Dashbord dashbord = null;
        private R_A r_A = null;
        private Suport suport = null;
        private ProfileM profileM = null;
        private Printing printing = null;
        private Mesages mesages = null;
        public List<OrderInfo> orderInfos = null;

        public int A_RWork(string typeR_A, ref string description, params string[] dataR_A)
        {
            r_A = new R_A();
            int stateA_R = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeR_A == "authorisation")
                {
                    stateA_R = r_A.Avthorization(dataR_A[0], dataR_A[1], dataR_A[2]);
                }
                else if (typeR_A == "RegistrationSt")
                {
                    stateA_R = r_A.RegistrationStart(dataR_A[0], dataR_A[1], dataR_A[2], ref description);
                }
                else if (typeR_A == "RegistrationEn")
                {
                    stateA_R = r_A.RegistrationComplete(dataR_A[0], ref description);
                }
                else if (typeR_A == "RemindPassword")
                {
                    stateA_R = r_A.RemindPassword(dataR_A[0]);
                }
            }
            r_A = null;
            return stateA_R;
        }

        public int SuportWork(string typeSuport, ref string description, params string[] dataSuport)
        {
            suport = new Suport();
            int stateA_R = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeSuport == "help")
                {
                    stateA_R = suport.HelpReq(dataSuport[0], dataSuport[1], dataSuport[2], ref description);
                }
            }
            suport = null;
            return stateA_R;
        }

        public string[] PofiletWork(string typeProfile, ref string description, ProfileData profile = null, params string[] dataProfileM)
        {
            profileM = new ProfileM();
            string[] profilear = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeProfile == "profileGet")
                {
                    string temp = null;
                    profilear = profileM.GetProfile(dataProfileM[0], dataProfileM[1], dataProfileM[2], ref temp);
                }
                else if (typeProfile == "ProfileSet")
                {
                    profilear = profileM.ProfileSet(dataProfileM[0], dataProfileM[1], dataProfileM[2], profile);
                }
                else if (typeProfile == "PostageBuyGet")
                {
                    profilear = profileM.GetPostage(dataProfileM[0], dataProfileM[1], dataProfileM[2]);
                }
                else if (typeProfile == "EBayAllowAccess")
                {
                    profilear = profileM.EBayAllowAccess(dataProfileM[0], dataProfileM[1], dataProfileM[2], dataProfileM[3], ref description);
                }
                else if (typeProfile == "EBayConfirm")
                {
                    profilear = profileM.EBayConfirm(dataProfileM[0], dataProfileM[1], dataProfileM[2], dataProfileM[3], ref description);
                }
            }
            else
            {
                profilear = new string[] { "1" };
            }
            profileM = null;
            return profilear;
        }

        public int PrintingWork(string typeSuport, ref string description, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr,
            double WeightOZ, bool SignatureConfirmation, bool DeliveryConfirmation, bool NoValidate, decimal InsuranceAmount, ref List<Carrier> carriers, params string[] dataSuport)
        {
            printing = new Printing();
            int Printing = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeSuport == "Options")
                {
                    Printing = printing.ShippingEstimate(dataSuport[1], dataSuport[0], dataSuport[2], WeightOZ, dim, SourceAddr, DestinationAddr,
                    DeliveryConfirmation, SignatureConfirmation, NoValidate, InsuranceAmount, ref description, ref carriers);
                }
            }
            printing = null;
            return Printing;
        }

        public int ShippingCreate(int ClientID, string Login, string Password, int LabelsQty, string ShippingMethod, string ShipToEmail, bool SignatureWaiver,
            double WeightOZ, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr, bool DeliveryConfirmation, bool SignatureConfirmation, bool NoValidate,
            bool EmailNotification, string OrderNumber, string ItemDescription, string PrinterID, decimal InsuranceAmount, ref string tracking, ref string description)
        {
            printing = new Printing();
            int Printing = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                Printing = printing.ShippingCreate(ClientID, Login, Password, LabelsQty, ShippingMethod, ShipToEmail, SignatureConfirmation, WeightOZ,
                    dim, SourceAddr, DestinationAddr, DeliveryConfirmation, SignatureConfirmation, NoValidate, EmailNotification, OrderNumber,
                    ItemDescription, PrinterID, InsuranceAmount, ref tracking, ref description);
            }
            printing = null;
            return Printing;
        }

        public int ShippingCreateOrder(int ClientID, string Login, string Password, int OrderID, int LabelsQty, string ShippingMethod, string ShipToEmail, bool SignatureWaiver,
            double WeightOZ, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr, bool DeliveryConfirmation, bool SignatureConfirmation, bool NoValidate,
            bool EmailNotification, string OrderNumber, string ItemDescription, string PrinterID, decimal InsuranceAmount, ref string tracking, ref string description)
        {
            printing = new Printing();
            int Printing = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                Printing = printing.ShippingCreateOrder(ClientID, Login, Password, OrderID, LabelsQty, ShippingMethod, ShipToEmail, SignatureConfirmation, WeightOZ,
                    dim, SourceAddr, DestinationAddr, DeliveryConfirmation, SignatureConfirmation, NoValidate, EmailNotification, OrderNumber,
                    ItemDescription, PrinterID, InsuranceAmount, ref tracking, ref description);
            }
            printing = null;
            return Printing;
        }

        public int DashbordWork(string typeDashbord, ref string description, int type, ref int countPage, ref int countOrder, params string[] dataDashbord)
        {
            dashbord = new Dashbord();
            if (orderInfos == null)
            {
                orderInfos = new List<OrderInfo>();
            }
            int stateDashbord = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeDashbord == "OrdersGet")
                {
                    stateDashbord = dashbord.GetDashbord(dataDashbord[0], dataDashbord[1], dataDashbord[2], type, ref countPage, ref description, ref orderInfos, ref countOrder);
                }
                else if (typeDashbord == "OrdersGet1")
                {
                    stateDashbord = dashbord.GetDashbord(dataDashbord[0], dataDashbord[1], dataDashbord[2], type, ref countPage, ref description, ref orderInfos, ref countOrder);
                }
            }
            dashbord = null;
            return stateDashbord;
        }

        public int MesagesWork(string typeMesages, ref string description, ref int totalResulte, ref List<Models.Messages> mesagess, params string[] dataMesages)
        {
            mesages = new Mesages();
            int stateMesges = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeMesages == "OptionsGet")
                {
                    mesages.OptionsGet(dataMesages[1], dataMesages[0], dataMesages[2], ref description);
                }
                else if (typeMesages == "MessagesGet")
                {
                    stateMesges = mesages.MessagesGet(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], Convert.ToInt32(dataMesages[3]), dataMesages[4], Convert.ToInt32(dataMesages[5]), ref description, ref mesagess, ref totalResulte);
                }
                else if(typeMesages == "MesageCountToDay")
                {
                    stateMesges = mesages.MessagesGetCount(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], 1, "", ref totalResulte);
                }
                else if (typeMesages == "Conversation")
                {
                    stateMesges = mesages.MessageHistoryGet(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], Convert.ToInt32(dataMesages[3]), 0, ref mesagess, ref totalResulte, ref description);
                }
            }
            mesages = null;
            return stateMesges;
        }

        public int MesagesWork(string typeMesages, ref int totalResulte, ref OrderInfo orderInfo, params string[] dataMesages)
        {
            mesages = new Mesages();
            int stateMesges = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeMesages == "Purchases")
                {
                    stateMesges = mesages.OrderHistoryGet(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], Convert.ToInt32(dataMesages[3]), Convert.ToInt32(dataMesages[4]), ref orderInfo);
                }
            }
            mesages = null;
            return stateMesges;
        }

        public int MesagesWork(string typeMesages, int Id, bool DisplayToPublic, bool EmailCopyToSender, ref string description, params string[] dataMesages)
        {
            mesages = new Mesages();
            int stateMesges = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeMesages == "SendMessageReply")
                {
                    stateMesges = mesages.SendMessageReply(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], Id, DisplayToPublic, EmailCopyToSender, dataMesages[3], ref description);
                }
            }
            mesages = null;
            return stateMesges;
        }
        
        public int MesagesWork(string typeMesages, int Id, ref string description, params string[] dataMesages)
        {
            mesages = new Mesages();
            int stateMesges = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeMesages == "MessageDelete")
                {
                    stateMesges = mesages.MessageDelete(Convert.ToInt32(dataMesages[1]), dataMesages[0], dataMesages[2], Id, ref description);
                }
            }
            mesages = null;
            return stateMesges;
        }
    }
}