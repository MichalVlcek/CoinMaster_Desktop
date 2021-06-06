using System.Globalization;
using CoinMaster.Model;

namespace CoinMaster.Utility
{
    public static class StringFormats
    {
        private const string Culture = "en-US";

        public static string RankFormat(int rank) => $"#{rank}";
        public static string PercentFormat(double percent) => $"{percent:0.##}%";

        public static string CurrencyFormat(decimal price)
            => price.ToString("C", CultureInfo.CreateSpecificCulture(Culture));
        
        public static string CurrencyFormat(decimal price, string symbol)
            => price.ToString("N", CultureInfo.CreateSpecificCulture(Culture)) + " " + symbol.ToUpper();
    }
}