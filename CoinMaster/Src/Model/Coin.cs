using System.Text.Json.Serialization;

namespace CoinMaster.Model
{
    public class Coin
    {
        [JsonPropertyName("image")] public string Icon { get; set; }
        [JsonPropertyName("market_cap_rank")] public int Rank { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("current_price")] public decimal Price { get; set; }
        [JsonPropertyName("market_cap")] public decimal MarketCap { get; set; }

        [JsonPropertyName("circulating_supply")]
        public decimal CirculatingSupply { get; set; }

        [JsonPropertyName("max_supply")] public decimal? MaxSupply { get; set; }
        [JsonPropertyName("ath")] public decimal Ath { get; set; }

        [JsonPropertyName("ath_change_percentage")]
        public double AthPercentChange { get; set; }

        [JsonPropertyName("atl")] public decimal Atl { get; set; }

        [JsonPropertyName("atl_change_percentage")]
        public double AtlPercentChange { get; set; }
    }
}