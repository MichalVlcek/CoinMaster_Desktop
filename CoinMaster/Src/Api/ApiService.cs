using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                .AddOrUpdateParameter("page", 1)
                .AddOrUpdateParameter("price_change_percentage", "24h,7d");
        }

        public static async Task<List<Coin>> LoadCoins(params Coin[] coins)
        {
            //TODO exception handling
            var joinedCoins = string.Join(',', coins.Select(c => c.Id));
            CoinsMarketsRequest.AddOrUpdateParameter("ids", joinedCoins);
            var response = await Client.ExecuteAsync(CoinsMarketsRequest);

            return JsonSerializer.Deserialize<List<Coin>>(response.Content)
                   ?? new List<Coin> { };
        }
    }
}