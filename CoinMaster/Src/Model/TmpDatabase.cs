using System.Collections.Generic;

namespace CoinMaster.Model
{
    public static class TmpDatabase
    {
        public static List<Coin> Coins { get; } = new();
        public static List<Transaction> Transactions { get; } = new();
    }
}