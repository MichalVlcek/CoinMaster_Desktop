using System;
using System.Globalization;
using CoinMaster.Utility;

namespace CoinMaster.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CoinId { get; set; }
        public Coin Coin { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public decimal CoinPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Description { get; set; }

        public decimal Cost => CoinPrice * Amount;
        public string AmountFormat => StringFormats.DecimalFormat(Amount);
        public string CostFormat => StringFormats.CurrencyFormat(Cost);
        public string TotalCostFormat => StringFormats.CurrencyFormat(Cost + Fee);
        public string DateFormat => Date.ToShortDateString();

        public string CoinPriceFormat => StringFormats.CurrencyFormat(CoinPrice);

        public string FeeFormat => StringFormats.CurrencyFormat(Fee);

        public static Transaction EmptyTransaction(Coin coin) => new Transaction
        {
            CoinId = coin.Id,
            Coin = coin,
            Type = TransactionType.BUY,
            Date = DateTime.Now.Date,
            CoinPrice = 0,
            Amount = 0,
            Fee = 0,
            Description = ""
        };

        public static bool IsEmptyTransaction(Transaction transaction) => transaction.Date == DateTime.Now.Date &&
                                                                          transaction.CoinPrice == 0 &&
                                                                          transaction.Amount == 0 &&
                                                                          transaction.Fee == 0 &&
                                                                          transaction.Description == "";
    }
}