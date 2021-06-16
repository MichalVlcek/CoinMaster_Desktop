using System.Collections.Generic;
using CoinMaster.Model;

namespace CoinMaster.Events
{
    public class TransactionsUpdatedEvent
    {
        public List<Transaction> Transactions { get; init; }
    }
}