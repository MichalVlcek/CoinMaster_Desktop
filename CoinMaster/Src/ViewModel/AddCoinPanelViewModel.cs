using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        public AddCoinPanelViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public void AddCoin()
        {
            TmpDatabase.Coins.Add(SelectedCoin);
        }
    }
}