using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using Vazoo1123.Models;

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

        public int MessagesGet(int ClientID, string Login, string Password, int Type, string Folder, ref int Page, ref string description, ref List<Models.Messages> mesages, ref int totalResulte)
        {
            string content = null;
            int profilear = 1;
            try
            {
                int state = 0;
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
                    Page++;
                    if (Page == 1)
                    {
                        state = MessagesGet(ClientID, Login, Password, Type, "", ref Page, ref description, ref mesages, ref totalResulte);
                    }
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int MessagesGetCount(int ClientID, string Login, string Password, int Type, string Folder, ref int totalResulte)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'Type':'{Type}','Folder':'{Folder}','Page':'{0}'" + "}";
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
                    ParseJson(content, ref profilear, ref totalResulte);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int OrderHistoryGet(int ClientID, string Login, string Password, int MessageID, int Page, ref OrderInfo orderInfo)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'MessageID':'{MessageID}','Page':'{Page}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/OrderHistoryGet", Method.POST);
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
                    ParseJson(content, ref profilear, ref orderInfo);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int MessageHistoryGet(int ClientID, string Login, string Password, int MessageID, int Page, ref List<Models.Messages> mesages, ref int totalResulte, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'MessageID':'{MessageID}','Page':'{Page}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/MessageHistoryGet", Method.POST);
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

        public int SendMessageReply(int ClientID, string Login, string Password, int OriginalMessageID, bool DisplayToPublic, bool EmailCopyToSender, string Body, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'OriginalMessageID':'{OriginalMessageID}','DisplayToPublic':'{DisplayToPublic}','EmailCopyToSender':'{EmailCopyToSender}','Body':'{Body}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/SendMessageReply", Method.POST);
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
                    ParseJson(content, ref profilear, ref description);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int MessageDelete(int ClientID, string Login, string Password, int MessageID, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'MessageID':'{MessageID}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/MessageDelete", Method.POST);
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
                    ParseJson(content, ref profilear, ref description);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        public int MessageSetRead(int ClientID, string Login, string Password, int MessageID, ref string description)
        {
            string content = null;
            int profilear = 1;
            try
            {
                string body = "{" + $"'ClientID':'{ClientID}','Login':'{Login}','Password':'{Password}', 'MessageID':'{MessageID}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/MessageSetRead", Method.POST);
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
                    ParseJson(content, ref profilear, ref description);
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            return profilear;
        }

        private void ParseJson(string jsonResponse, ref int state, ref int totalResulte)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            totalResulte = objJsonRespons.First
                .First.Value<int>("totalResults");
            if (stateResponse == "success")
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
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
                if ( mesages != null)
                {
                    mesages.AddRange(JsonConvert.DeserializeObject<List<Models.Messages>>(objJsonRespons.
                            First.First.SelectToken("Messages").ToString()));
                }
                else
                {
                    mesages = JsonConvert.DeserializeObject<List<Models.Messages>>(objJsonRespons.
                            First.First.SelectToken("Messages").ToString());
                }
                state = 3;
            }
            else
            {
                state = 2;
            }
        }

        private void ParseJson(string jsonResponse, ref int state, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            if (stateResponse == "success")
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
        }


        private void ParseJson(string jsonResponse, ref int state, ref OrderInfo orderInfo)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            if (stateResponse == "success")
            {
                orderInfo = new OrderInfo();
                string s = objJsonRespons.
                         First.First.SelectToken("Orders").ToString();
                var orderInfos = new List<OrderInfo>();
                orderInfos.AddRange(JsonConvert.DeserializeObject<List<OrderInfo>>(objJsonRespons.
                        First.First.SelectToken("Orders").ToString()));
                if (orderInfos.Count != 0)
                {
                    orderInfo = orderInfos[0];
                }
                else
                {
                    orderInfo = null;
                }
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
    }
}