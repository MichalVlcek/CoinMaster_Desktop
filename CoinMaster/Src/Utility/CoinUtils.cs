using System;
using System.Collections.Generic;
using System.Linq;
using CoinMaster.Model;

namespace CoinMaster.Utility
{
    public static class CoinUtils
    {
        public static decimal CalculatePriceChange(decimal price, double percent)
        {
            var oldPrice = price * (100 / (100 + (decimal) percent));
            return price - oldPrice;
        }

        /// <summary>
        /// Counts total amount of held coin
        /// Formula: Sum of all buy transactions - Sum of Sell transactions - Sum of Send_Out transactions - Coin Fees
        /// </summary>
        public static decimal CountHoldings(IEnumerable<Transaction> transactions)
        {
            var buyTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.BUY));
            var sellTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.SELL));
            var receiveTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.RECEIVE));
            var sendTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.SEND));

            return buyTransactions + receiveTransactions - sellTransactions - sendTransactions -
                   SumOfFees(transactions);
        }

        /// <summary>
        /// Counts current value of held coins
        /// Formula: Sum of all holdings * current price of coin
        /// </summary>
        public static decimal CountHoldingsValue(IEnumerable<Transaction> transactions, decimal coinPrice) =>
            CountHoldings(transactions) * coinPrice;

        /// <summary>
        /// Counts current value for all held coins
        /// Formula: Sum(Sum of coin holdings * current price of coin)
        /// </summary>
        //TODO uncomment when coins have id
        // private static decimal CountHoldingsValue(IEnumerable<Transaction> transactions, IEnumerable<Coin> coins) =>
        //     coins.Select(coin => CountHoldingsValue(
        //         transactions.Where(t => t.CoinId == coin.Id),
        //         coin.Price
        //     )).Sum();

        /// <summary>
        /// Counts value of held coins in past
        /// Formula: Sum(Sum of coin holdings * past price of coin)
        /// </summary>
        public static decimal CountHoldingsValueHistorical(
            IEnumerable<Transaction> transactions,
            Dictionary<string, decimal> coinPrices,
            DateTime date) =>
            coinPrices.Select(p =>
                CountHoldingsValue(
                    transactions.Where(t => t.CoinId == p.Key && t.Date <= date),
                    p.Value
                )
            ).Sum();

        /// <summary>
        /// Counts total amount of money spent on the currently held coins
        /// Formula: Sum of cost of all Buy Transactions - Sum of all Sell transactions + Sum of All Fees
        /// </summary>
        public static decimal CountTotalCost(IEnumerable<Transaction> transactions)
        {
            var buyTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.BUY));
            var sellTransactions = SumOfCoinAmount(GetTransactionsByType(transactions, TransactionType.SELL));

            return buyTransactions - sellTransactions + SumOfFees(transactions);
        }

        /// <summary>
        /// Counts average cost of all transactions
        /// Formula: Sum of all coin prices / count of transactions
        /// </summary>
        public static decimal CountAverageCost(IEnumerable<Transaction> transactions) =>
            transactions.Count() > 0 ? transactions.Select(t => t.CoinPrice).Sum() / transactions.Count() : 0;


        /// <summary>
        /// Counts difference between current value of coins and total cost of coins
        /// Formula: Current holdings value - Total cost
        /// </summary>
        public static decimal CountProfitOrLoss(IEnumerable<Transaction> transactions, decimal coinPrice) =>
            CountHoldingsValue(transactions, coinPrice) - CountTotalCost(transactions);

        /// <summary>
        /// Counts the percentage difference between value of current holdings and total cost
        /// Formula: Current holdings value / Total Cost - 1
        /// </summary>
        public static double CountPercentChange(IEnumerable<Transaction> transactions, decimal coinPrice)
        {
            var totalCost = (double) CountTotalCost(transactions);
            if (totalCost == 0)
            {
                return 0;
            }

            return (double) CountHoldingsValue(transactions, coinPrice) / totalCost - 1;
        }

        /// <summary>
        /// Counts the percentage difference between value of current holdings and total cost
        /// Formula: Current holdings value / Total Cost - 1
        /// </summary>
        public static decimal CountPercentageChangeForAll(decimal totalHoldings, decimal holdingsHistorical)
        {
            if ((totalHoldings == 0 && holdingsHistorical == 0) || holdingsHistorical == 0)
            {
                return 0;
            }

            return totalHoldings / holdingsHistorical - 1;
        }

        private static IEnumerable<Transaction> GetTransactionsByType(
            IEnumerable<Transaction> transactions,
            TransactionType type) =>
            transactions.Where(t => t.Type == type);

        private static decimal SumOfFees(IEnumerable<Transaction> transactions) =>
            transactions.Select(t => t.Fee).Sum();

        private static decimal SumOfCoinAmount(IEnumerable<Transaction> transactions) =>
            transactions.Select(t => t.Amount).Sum();

        private static decimal SumOfCosts(IEnumerable<Transaction> transactions) =>
            transactions.Select(t => t.Cost).Sum();
    }
}