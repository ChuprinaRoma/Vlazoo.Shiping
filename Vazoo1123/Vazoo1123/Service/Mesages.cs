using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Vazoo1123.Service
{
    public class Mesages
    {
        public int OptionsGet(string ClientID, string Login, string Password, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'OptionType':'{2}'" + "}";
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
                    //ParseJson(content, out profilear, ref description, ref carriers);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int MessagesGet(int ClientID, string Login, string Password, int Type, string Folder, int Page, ref string description, ref List<Models.Messages> mesages, ref int totalResulte)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'Type':'{Type}','Folder':'{Folder}','Page':'{Page}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/MessagesGet", Method.POST);
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
                    ParseJson(content, ref profilear, ref description, ref mesages, ref totalResulte);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        private void ParseJson(string jsonResponse, ref int state, ref string description, ref List<Models.Messages> mesages, ref int totalResulte)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            totalResulte = objJsonRespons.First
                .First.Value<int>("totalResults");
            if (stateResponse == "success")
            {
                mesages = new List<Models.Messages>();
                mesages.AddRange(JsonConvert.DeserializeObject<List<Models.Messages>>(objJsonRespons.
                        First.First.SelectToken("Messages").ToString()));
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
    }
}