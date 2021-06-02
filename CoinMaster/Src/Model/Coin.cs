namespace CoinMaster.Src.Model
{
    public class Coin
    {
        public string Icon { get; }
        public int Rank { get; }
        public string Name { get; }
        public string Symbol { get; }
        public decimal Price { get; }
        public decimal MarketCap { get; }
        public decimal Supply { get; }

        public Coin(string icon, int rank, string name, string symbol, decimal price, decimal marketCap, decimal supply)
        {
            Icon = icon;
            Rank = rank;
            Name = name;
            Symbol = symbol;
            Price = price;
            MarketCap = marketCap;
            Supply = supply;
        }
    }
}