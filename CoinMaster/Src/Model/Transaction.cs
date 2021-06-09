using System;

namespace CoinMaster.Model
{
    public class Transaction
    {
        public TransactionType Type { get; init; }
        public string CoinId { get; init; }
        public DateTime Date { get; init; }
        public decimal Cost { get; init; }
        public decimal Amount { get; init; }
        public decimal Fee { get; init; }
        public string Description { get; init; }

        public decimal TotalCost => Cost + Fee;
    }
}