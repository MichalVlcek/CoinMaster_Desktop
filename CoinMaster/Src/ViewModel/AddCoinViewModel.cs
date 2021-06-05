using System.ComponentModel;
using CoinMaster.Api;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinViewModel : Screen
    {
        public AddCoinPanelViewModel AddCoinPanel { get; set; }

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
                AddCoinPanel.SelectedCoin = value;
            }
        }

        public AddCoinViewModel()
        {
            AddCoinPanel = new AddCoinPanelViewModel();
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            Coins = await ApiService.LoadCoins();
        }
    }
}