using Stylet;

namespace CoinMaster.ViewModel
{
    public class CoinOverviewViewModel : AbstractCoinSubscriber
    {
        public CoinOverviewViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}