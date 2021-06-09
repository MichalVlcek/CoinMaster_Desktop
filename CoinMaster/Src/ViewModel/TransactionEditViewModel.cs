using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionEditViewModel : AbstractCoinSubscriber
    {
        public TransactionEditViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}