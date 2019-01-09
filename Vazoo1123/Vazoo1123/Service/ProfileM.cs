using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using Vazoo1123.ViewModels.Profile;

namespace Vazoo1123.Service
{
    class ProfileM
    {
        public string[] GetProfile(string clientID, string login, string password, ref string description)
        {
            string content = null;
            string[] profile = null;
            try
            {
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ProfileGet", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new string[] { "4" };
                }
                else
                {
                    ParseJson(content, ref profile, ref description);
                }
            }
            catch (Exception e)
            {
                return new string[] { "2" };
            }
            return profile;
        }
        
        public string[] GetPostage(string clientID, string login, string password)
        {
            string content = null;
            string[] profile = null;
            try
            {
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ProfileGet", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new string[] { "4" };
                }
                else
                {
                    ParseJson2(content, ref profile);
                }
            }
            catch (Exception e)
            {
                return new string[] { "2" };
            }

            return profile;
        }

        public string[] ProfileSet(string clientID, string login, string password, ProfileData profile)
        {
            string content = null;
            string[] profilear = null;
            try
            {
                string profileJsonStr = JsonConvert.SerializeObject(profile);
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}','Profile':{profileJsonStr}" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/ProfileSet", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                content = response.Content;
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new string[] { "4" };
                }
                else
                {
                    ParseJson1(content, ref profilear);
                }
            }
            catch (Exception e)
            {
                return new string[] { "2" };
            }
            return profilear;
        }

        public string[] EBayAllowAccess(string clientID, string login, string password, string eBayUserID, ref string description)
        {
            string content = null;
            string[] profile = null;
            try
            {
                string[] infoProfile = GetProfile(clientID, login, password, ref description);
                if (Convert.ToInt32(infoProfile[0]) == 3)
                {
                    string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}','EBayUserID':'{eBayUserID}','PayPalEmail':'{infoProfile[17]}'" + "}";
                    RestClient client = new RestClient("https://vlazoo.com");
                    RestRequest request = new RestRequest("/WS/Mobile.asmx/EBayAllowAccess", Method.POST);
                    request.AddHeader("Accept", "application/json");
                    request.Parameters.Clear();
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    content = response.Content;
                    if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new string[] { "4" };
                    }
                    else
                    {
                        ParseJson3(content, ref profile, ref description);
                    }
                }
                else
                {
                    return new string[] { infoProfile[0].ToString() };
                }
            }
            catch (Exception e)
            {
                return new string[] { "2" };
            }
            return profile;
        }
        
        public string[] EBayConfirm(string clientID, string login, string password, string sessionID, ref string description)
        {
            string content = null;
            string[] profile = null;
            try
            {
                    string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}','SessionID':'{sessionID}'" + "}";
                    RestClient client = new RestClient("https://vlazoo.com");
                    RestRequest request = new RestRequest("/WS/Mobile.asmx/EBayConfirm", Method.POST);
                    request.AddHeader("Accept", "application/json");
                    request.Parameters.Clear();
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    content = response.Content;
                    if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new string[] { "4" };
                    }
                    else
                    {
                    ParseJson4(content, ref profile, ref description);
                    }
            }
            catch (Exception e)
            {
                return new string[] { "2" };
            }

            return profile;
        }

        private void ParseJson4(string jsonResponse, ref string[] profile, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First.First
                .Value<string>("description");
            if (stateResponse == "success")
            {
                profile = new string[3];
                profile[0] = "3";
            }
            else
            {
                profile = new string[1];
                profile[0] = "2";
            }
        }
        
        private void ParseJson3(string jsonResponse, ref string[] profile, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First.First
                .Value<string>("description");
            if (stateResponse == "success")
            {
                profile = new string[3];
                profile[0] = "3";
                profile[1] = objJsonRespons.First
                    .First.Value<string>("URL");
                profile[2] = objJsonRespons.First
                    .First.Value<string>("sessionID");
            }
            else
            {
                profile = new string[1];
                profile[0] = "2";
            }
        }

        private void ParseJson2(string jsonResponse, ref string[] profile)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            if (stateResponse == "success")
            {
                profile = new string[2];
                profile[0] = "3";
                profile[1] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("PostageBalance");
            }
            else
            {
                profile = new string[1];
                profile[0] = "2";
            }
        }

        private void ParseJson1(string jsonResponse, ref string[] profile)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            if(stateResponse == "success")
            {
                profile = new string[] { "3" };
            }
            else
            {
                profile = new string[] { "2" };
            }
        }
    
        private void ParseJson(string jsonResponse, ref string[] profile, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First.First
                .Value<string>("description");
            if (stateResponse == "success")
            {
                profile = new string[20];
                profile[0] = "3";
                profile[1] = objJsonRespons.First
                .First.Value<string>("id");
                profile[2] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("CompanyName");
                profile[3] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("UserName");
                profile[5] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("ContactName");
                profile[6] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("ClientEmail");
                profile[7] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("ClientPhone");
                profile[8] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseContactName");
                profile[9] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("LegalName");
                profile[10] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseAddress1") +", "+ objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseAddress2");
                profile[11] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseFloor");
                profile[12] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseCity");
                profile[13] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseState");
                profile[14] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehouseZip");
                profile[15] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("WarehousePhone");
                profile[16] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("EBayUserID");
                profile[17] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("PayPalEmail");
                profile[18] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("PostageBalance");
                profile[19] = objJsonRespons.First
                    .First.SelectToken("Profile")
                    .Value<string>("TokenCreated");
            }
            else
            {
                profile = new string[1];
                profile[0] = "2";
            }
        }
    }
}