using System;
using CoinMaster.Utility;

namespace CoinMaster.Model
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public string CoinId { get; init; }
        public DateTime Date { get; set; }
        public decimal CoinPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Description { get; set; }

        private decimal Cost => CoinPrice * Amount;
        public string CostFormat => StringFormats.CurrencyFormat(Cost);
        public string TotalCostFormat => StringFormats.CurrencyFormat(Cost + Fee);
        public string DateFormat => Date.ToShortDateString();
        public string CoinPriceFormat => StringFormats.CurrencyFormat(CoinPrice);
        public string FeeFormat => StringFormats.CurrencyFormat(Fee);

        public static Transaction EmptyTransaction => new Transaction
        {
            Type = TransactionType.BUY,
            CoinId = "",
            Date = DateTime.Now,
            CoinPrice = 0,
            Amount = 0,
            Fee = 0,
            Description = ""
        };
    }
}