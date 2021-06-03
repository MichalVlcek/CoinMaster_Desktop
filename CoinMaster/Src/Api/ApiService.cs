﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using CoinMaster.Model;
using RestSharp;

namespace CoinMaster.Api
{
    public static class ApiService
    {
        private const string BaseUrl = "https://api.coingecko.com/api/v3/";

        private static RestClient Client { get; } = new RestClient(BaseUrl);

        private static RestRequest CoinsMarketsRequest { get; } = new RestRequest("/coins/markets", DataFormat.Json);

        static ApiService()
        {
            CoinsMarketsRequest
                .AddOrUpdateParameter("vs_currency", "usd")
                .AddOrUpdateParameter("order", "market_cap_desc")
                .AddOrUpdateParameter("per_page", 250)
                .AddOrUpdateParameter("page", 1);
        }

        public static async Task<BindingList<Coin>> LoadCoins()
        {
            //TODO exception handling
            var response = await Client.ExecuteAsync(CoinsMarketsRequest);

            var convertedContent =
                JsonSerializer.Deserialize<List<Coin>>(response.Content)
                ?? new List<Coin> { };

            return new BindingList<Coin>(convertedContent);
        }
    }
}