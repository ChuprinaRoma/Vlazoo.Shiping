using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Vazoo1123.Models;
using Vazoo1123.ViewModels.Printing.Models;

namespace Vazoo1123.Service
{
    public class Printing
    {
        public int ShippingEstimate(string clientID, string login, string password,
            double WeightOZ, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr, bool DeliveryConfirmation, 
            bool SignatureConfirmation, bool NoValidate, decimal InsuranceAmount, ref string description, ref List<Carrier> carriers)
        {
            string content = null;
            int profilear;
            try
            {
                string dimJsonStr = JsonConvert.SerializeObject(dim);
                string DestinationAddrJsonStr = JsonConvert.SerializeObject(DestinationAddr);
                string SourceAddrJsonStr = JsonConvert.SerializeObject(SourceAddr);
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}','WeightOZ':'{WeightOZ}','dim':{dimJsonStr}," +
                    $"'SourceAddr':{SourceAddrJsonStr},'DestinationAddr':{DestinationAddrJsonStr},'DeliveryConfirmation':'{DeliveryConfirmation}','SignatureConfirmation':'{SignatureConfirmation}'," +
                    $"'NoValidate':'{NoValidate}','InsuranceAmount':'{InsuranceAmount}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ShippingEstimate", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 4;
                }
                else
                {
                    ParseJson(content, out profilear, ref description, ref carriers);
                }
            }
            catch (Exception e)
            {
                return 2;
            }
            return profilear;
        }

        public int ShippingEstimateOrderint (int ClientID, string Login, string Password, int OrderID, ref List<Carrier> carriers, ref string description)
        {
            string content = null;
            int profilear;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}','OrderID':'{OrderID}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ShippingEstimateOrder", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 4;
                }
                else
                {
                    ParseJson(content, out profilear, ref description, ref carriers);
                }
            }
            catch (Exception e)
            {
                return 2;
            }
            return profilear;
        }

        public int ShippingCreate(int ClientID, string Login, string Password, int LabelsQty, string ShippingMethod, string ShipToEmail, bool SignatureWaiver,
            double WeightOZ, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr, bool DeliveryConfirmation, bool SignatureConfirmation, bool NoValidate,
            bool EmailNotification, string OrderNumber, string ItemDescription, string PrinterID, decimal InsuranceAmount, ref string tracking, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string dimJsonStr = JsonConvert.SerializeObject(dim);
                string DestinationAddrJsonStr = JsonConvert.SerializeObject(DestinationAddr);
                string SourceAddrJsonStr = JsonConvert.SerializeObject(SourceAddr);
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}','LabelsQty':'{LabelsQty}','ShippingMethod':'{ShippingMethod}'," +
                    $"'ShipToEmail':'{ShipToEmail}','SignatureWaiver':'{SignatureWaiver}','WeightOZ':'{WeightOZ}','dim':{dimJsonStr},'SourceAddr':{SourceAddrJsonStr}," +
                    $"'DestinationAddr':{DestinationAddrJsonStr},'DeliveryConfirmation':'{DeliveryConfirmation}','SignatureConfirmation':'{SignatureConfirmation}'," +
                    $"'NoValidate':'{NoValidate}','EmailNotification':'{EmailNotification}','OrderNumber':'{OrderNumber}','ItemDescription':'{ItemDescription}'," +
                    $"'PrinterID':'{128}','InsuranceAmount':'{InsuranceAmount}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ShippingCreate", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //string strResponse = JsonConvert.SerializeObject(response);
                //string strReqvest = JsonConvert.SerializeObject(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 4;
                }
                else
                {
                    ParseJson1(content, out profilear, ref description, ref tracking);
                }
            }
            catch (Exception e)
            {
                return 2;
            }
            return profilear;
        }

        public int ShippingCreateOrder(int ClientID, string Login, string Password, int OrderID, int LabelsQty, string ShippingMethod, string ShipToEmail, bool SignatureWaiver,
            double WeightOZ, CDimensions dim, Models.CAddressBase SourceAddr, Models.CAddressBase DestinationAddr, bool DeliveryConfirmation, bool SignatureConfirmation, bool NoValidate,
            bool EmailNotification, string OrderNumber, string ItemDescription, string PrinterID, decimal InsuranceAmount, ref string tracking, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string dimJsonStr = JsonConvert.SerializeObject(dim);
                string DestinationAddrJsonStr = JsonConvert.SerializeObject(DestinationAddr);
                string SourceAddrJsonStr = JsonConvert.SerializeObject(SourceAddr);
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','OrderID':'{OrderID}','Password':'{Password}','LabelsQty':'{LabelsQty}','ShippingMethod':'{ShippingMethod}'," +
                    $"'ShipToEmail':'{ShipToEmail}','SignatureWaiver':'{SignatureWaiver}','WeightOZ':'{WeightOZ}','dim':{dimJsonStr},'SourceAddr':{SourceAddrJsonStr}," +
                    $"'DestinationAddr':{DestinationAddrJsonStr},'DeliveryConfirmation':'{DeliveryConfirmation}','SignatureConfirmation':'{SignatureConfirmation}'," +
                    $"'NoValidate':'{NoValidate}','EmailNotification':'{EmailNotification}','OrderNumber':'{OrderNumber}','ItemDescription':'{ItemDescription}'," +
                    $"'PrinterID':'{128}','InsuranceAmount':'{InsuranceAmount}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ShippingCreateOrder", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 4;
                }
                else
                {
                    ParseJson1(content, out profilear, ref description, ref tracking);
                }
            }
            catch (Exception e)
            {
                return 2;
            }
            return profilear;
        }

        public int OptionsGet(int ClientID, string Login, string Password, ref List<string[]> dropDwnChooseRemovePrinter)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'OptionType':'{1}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/OptionsGet", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 4;
                }
                else
                {
                    ParseJson(content, out profilear, ref dropDwnChooseRemovePrinter);
                }
            }
            catch (Exception e)
            {
                return 2;
            }
            return profilear;
        }

        private void ParseJson(string jsonResponse, out int profile, ref List<string[]> dropDwnChooseRemovePrinter)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            if (stateResponse == "success")
            {
                dropDwnChooseRemovePrinter = JsonConvert.DeserializeObject<List<string[]>>(objJsonRespons
                    .First.First.Value<object>("Options").ToString());
                profile = 3;
            }
            else
            {
                profile = 2;
            }
        }

        private void ParseJson1(string jsonResponse, out int profile, ref string description, ref string tracking)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            if (stateResponse == "success")
            {
                tracking = objJsonRespons.First
                .First.Value<string>("tracking");
                profile = 3;
            }
            else
            {
                profile = 2;
            }
        }

        private void ParseJson(string jsonResponse, out int profile, ref string description, ref List<Carrier> carriers)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            if (stateResponse == "success")
            {
                carriers = JsonConvert.DeserializeObject<List<Carrier>>(objJsonRespons.
                        First.First.SelectToken("Carriers").ToString());
                profile = 3;
                SetId(ref carriers);
            }
            else
            {
                profile = 2;
            }
        }

        private void SetId(ref List<Carrier> carriers)
        {
            for (int i = 0; i < carriers.Count; i++)
            {
                carriers[i].Id = i + 1;
            }
        }
    }
}