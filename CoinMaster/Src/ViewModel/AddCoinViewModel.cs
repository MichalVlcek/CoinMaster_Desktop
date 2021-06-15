using System.ComponentModel;
using CoinMaster.Api;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinViewModel : Screen
    {
        public AddCoinPanelViewModel AddCoinPanel { get; set; }

        private readonly IEventAggregator events;

        private BindingList<Coin> _coins;
        public BindingList<Coin> Coins
        {
            get => _coins;
            private set => SetAndNotify(ref _coins, value);
        }

        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                SetAndNotify(ref _selectedCoin, value);
                events.Publish(new ElementSelectedEvent<Coin> {Element = SelectedCoin});
            }
        }

        public AddCoinViewModel(AddCoinPanelViewModel addCoinPanel, IEventAggregator events)
        {
            this.events = events;
            AddCoinPanel = addCoinPanel;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            Coins = await ApiService.LoadCoins();
        }
    }
}