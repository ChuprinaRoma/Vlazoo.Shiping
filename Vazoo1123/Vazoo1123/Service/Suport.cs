using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace Vazoo1123.Service
{
    class Suport
    {
        public int HelpReq(string email, string name, string message,ref string description)
        {
            IRestResponse response = null;
            string content;
            try
            {
                string body = "{" + $"'Email':'{email}','Name':'{name}','Message':'{message}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/HelpRequest", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request);
                content = response.Content;
            }
            catch(Exception)
            {
                return 2;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return parseJson(content, ref description) == "success" ? 3 : 2;
            }
        }

        private string parseJson(string jsonResponse, ref string description)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            return stateResponse;
        }
    }
}