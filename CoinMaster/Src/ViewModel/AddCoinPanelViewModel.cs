using System.Drawing;
using System.Windows.Data;
using System.Windows.Markup;
using CoinMaster.Events;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : Screen, IHandle<CoinSelectedEvent>
    {
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            private set => SetAndNotify(ref _selectedCoin, value);
        }

        public AddCoinPanelViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }
        
        public void Handle(CoinSelectedEvent message)
        {
            SelectedCoin = message.Coin;
        }

        public void AddCoin()
        {
            TmpDatabase.Coins.Add(SelectedCoin);
        }
    }
}