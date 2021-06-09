using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        public AddCoinPanelViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void AddCoin()
        {
            TmpDatabase.Coins.Add(SelectedCoin);
        }
    }
}