using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace Vazoo1123.Service
{
    public class R_A
    {
        private static string idc;

        public int Avthorization(string clientID, string login, string password)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ValidateCredentials", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 2;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return parseJson(content) == "success" ? 3 : 2;
            }
        }

        public int RemindPassword(string login)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string body = "{" + $"'Login':'{login}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/RemindPassword", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 2;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return parseJson(content) == "success" ? 3 : 2;
            }
        }

        public int RegistrationStart(string email, string companyName, string password, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string body = "{" + $"'Email':'{email}','CompanyName':'{companyName}','Password':'{password}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/RegistrationStart", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 2;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return parseJson1(content, ref description) == "success" ? 3 : 2;
            }
        }

        public int RegistrationComplete(string validationCode, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string body = "{" + $"'id':'{idc}','ValidationCode':'{validationCode}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/RegistrationComplete", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 2;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return parseJson2(content, ref description) == "success" ? 3 : 2;
            }
        }

        private string parseJson2(string jsonResponse, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            return stateResponse;
        }

        private string parseJson1(string jsonResponse, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            idc = objJsonRespons.First
                .First.Value<string>("id");
            description = objJsonRespons.First
                .First.Value<string>("description"); 
            return stateResponse;
        }

        private string parseJson(string jsonResponse)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            return stateResponse;
        }
    }
}