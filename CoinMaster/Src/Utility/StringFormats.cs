using System.Globalization;
using CoinMaster.Model;

namespace CoinMaster.Utility
{
    public static class StringFormats
    {
        private const string Culture = "en-US";

        public static string RankFormat(int rank) => $"#{rank}";
        public static string PercentFormat(double percent) => $"{PlusWhenPositive(percent)}{percent:0.##}%";

        public static string CurrencyFormat(decimal price)
            => $"{FormatPrice(price, "C")}";

        public static string CurrencyFormat(decimal price, string symbol) =>
            $"{FormatPrice(price, "N")} {symbol.ToUpper()}";

        public static string DecimalFormat(decimal number) =>
            number.ToString("0.#########", CultureInfo.InvariantCulture);

        private static string PlusWhenPositive(double num) => num > 0 ? "+" : "";

        private static string FormatPrice(decimal price, string format) =>
            price.ToString(format, CultureInfo.CreateSpecificCulture(Culture));
    }
}