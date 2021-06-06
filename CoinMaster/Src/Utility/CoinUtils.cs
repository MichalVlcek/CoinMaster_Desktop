namespace CoinMaster.Utility
{
    public static class CoinUtils
    {
        public static decimal CalculatePriceChange(decimal price, double percent)
        {
            var oldPrice = price * (100 / (100 + (decimal) percent));
            return price - oldPrice;
        } 
    }
}