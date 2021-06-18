using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using CoinMaster.Utility;

namespace CoinMaster.Model
{
    public class Coin
    {
        [Key] [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("image")] public string Icon { get; set; }
        [JsonPropertyName("market_cap_rank")] public int Rank { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set;  }
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

        [JsonPropertyName("price_change_percentage_24h_in_currency")]
        public double? PriceChangePercent24H { get; set; }

        [JsonPropertyName("price_change_percentage_7d_in_currency")]
        public double? PriceChangePercent7D { get; set; }

        public ICollection<Transaction> Transaction { get; set; } = new List<Transaction>();

        public Coin(string id, string icon, int rank, string name, string symbol, decimal price, decimal marketCap,
            decimal circulatingSupply, decimal? maxSupply, decimal ath, double athPercentChange, decimal atl,
            double atlPercentChange, double? priceChangePercent24H, double? priceChangePercent7D)
        {
            Id = id;
            Icon = icon;
            Rank = rank;
            Name = name;
            Symbol = symbol;
            Price = price;
            MarketCap = marketCap;
            CirculatingSupply = circulatingSupply;
            MaxSupply = maxSupply;
            Ath = ath;
            AthPercentChange = athPercentChange;
            Atl = atl;
            AtlPercentChange = atlPercentChange;
            PriceChangePercent24H = priceChangePercent24H;
            PriceChangePercent7D = priceChangePercent7D;
        }

        public string HeldValue => StringFormats.CurrencyFormat(CoinUtils.CountHoldingsValue(Transaction, Price));
        public string HeldAmount => StringFormats.CurrencyFormat(Transaction.Select(t => t.Amount).Sum(), Symbol);
        public string Title => $"{Name} - {Symbol.ToUpper()}";
        public string RankFormat => StringFormats.RankFormat(Rank);
        public string PriceFormat => StringFormats.CurrencyFormat(Price);
        public string AthFormat => StringFormats.CurrencyFormat(Ath, Symbol);
        public string AtlFormat => StringFormats.CurrencyFormat(Atl, Symbol);
        public string AthChangeFormat => StringFormats.PercentFormat(AthPercentChange);
        public string AtlChangeFormat => StringFormats.PercentFormat(AtlPercentChange);
        public string MarketCapFormat => StringFormats.CurrencyFormat(MarketCap);

        public string CirculatingSupplyFormat => StringFormats.CurrencyFormat(CirculatingSupply, Symbol);

        public string MaximumSupplyFormat => StringFormats.CurrencyFormat(MaxSupply ?? 0, Symbol);
        public string PriceChangePercent24HFormat => StringFormats.PercentFormat(PriceChangePercent24H ?? 0);
        public string PriceChangePercent7DFormat => StringFormats.PercentFormat(PriceChangePercent7D ?? 0);

        public string PriceChange24HFormat =>
            StringFormats.CurrencyFormat(CoinUtils.CalculatePriceChange(Price, PriceChangePercent24H ?? 0));

        public string PriceChange7DFormat =>
            StringFormats.CurrencyFormat(CoinUtils.CalculatePriceChange(Price, PriceChangePercent7D ?? 0));
    }
}