using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class CoinOverviewViewModel : Screen, IHandle<CoinSelectedEvent>
    {
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            private set => SetAndNotify(ref _selectedCoin, value);
        }

        public CoinOverviewViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }
        
        public void Handle(CoinSelectedEvent message)
        {
            SelectedCoin = message.Coin;
        }
    }
}