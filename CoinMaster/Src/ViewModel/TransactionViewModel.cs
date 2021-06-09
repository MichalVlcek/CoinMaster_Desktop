using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionViewModel : AbstractCoinSubscriber
    {
        public TransactionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}