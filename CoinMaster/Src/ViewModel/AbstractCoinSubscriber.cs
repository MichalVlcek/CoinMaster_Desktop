using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AbstractCoinSubscriber : Screen, IHandle<CoinSelectedEvent>
    {
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            private set => SetAndNotify(ref _selectedCoin, value);
        }
        
        public void Handle(CoinSelectedEvent message)
        {
            SelectedCoin = message.Coin;
        }
    }
}