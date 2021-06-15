using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AbstractCoinSubscriber : Screen, IHandle<SelectedElementEvent<Coin>>
    {
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            private set => SetAndNotify(ref _selectedCoin, value);
        }
        
        public AbstractCoinSubscriber(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }
        
        public void Handle(SelectedElementEvent<Coin> message)
        {
            SelectedCoin = message.Element;
        }
    }
}