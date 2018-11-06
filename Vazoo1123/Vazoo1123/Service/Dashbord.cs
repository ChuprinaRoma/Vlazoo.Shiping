﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using Vazoo1123.Models;

namespace Vazoo1123.Service
{
    class Dashbord
    {
        public int GetDashbord(string clientID, string login, string password, int type, ref int page, ref string description,
            ref List<OrderInfo> orderInfos)
        {
            string content = null;
            int state = 0;
            try
            {
                int countOrder;
                string body = "{" + $"'ClientID':'{clientID}','Login':'{login}','Password':'{password}','Type':'{type}','Page':'{page}'" + "}";
                RestClient client = new RestClient("https://vlazoo.com");
                RestRequest request = new RestRequest("/WS/Mobile.asmx/OrdersGet", Method.POST);
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
                    ParseJson(content, ref state, ref description,ref orderInfos, out countOrder, ref page);
                    page++;
                    if (page == 1 && countOrder >= 99)
                    {
                        state = GetDashbord(clientID, login, password, type, ref page, ref description, ref orderInfos);
                    }
                }
            }
            catch (Exception e)
            {
                return 2;
            }

            if(state != 3)
            {
                page = 1;
            }

            return state;
        }

        private void ParseJson(string jsonResponse, ref int state, ref string description, ref List<OrderInfo> orderInfos, out int countOrder, ref int countPage)
        {
            string stateResponse = null;
            JObject objJsonRespons = JObject.Parse(jsonResponse);
            stateResponse = objJsonRespons.First
                .First.Value<string>("status");
            description = objJsonRespons.First
                .First.Value<string>("description");
            countOrder = objJsonRespons.First
                .First.Value<int>("totalResults");
            if (stateResponse == "success")
            {
                if (countPage > 0)
                {
                    orderInfos.AddRange(JsonConvert.DeserializeObject<List<OrderInfo>>(objJsonRespons.
                        First.First.SelectToken("Orders").ToString()));
                }
                else
                {
                    orderInfos = JsonConvert.DeserializeObject<List<OrderInfo>>(objJsonRespons.
                        First.First.SelectToken("Orders").ToString());
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