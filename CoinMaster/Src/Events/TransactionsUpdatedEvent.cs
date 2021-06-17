using CoinMaster.Model;

namespace CoinMaster.Events
{
    public class TransactionsUpdatedEvent
    {
        public Transaction Transaction { get; init; }
    }
}