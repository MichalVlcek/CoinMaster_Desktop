using System.ComponentModel;
using CoinMaster.Api;
using CoinMaster.Core;
using CoinMaster.Model;
using CoinMaster.Src.Core;

namespace CoinMaster.ViewModel
{
    public class AddCoinViewModel : ObservableObject
    {
        public AddCoinPanelViewModel AddCoinPanelViewModel { get; }
        
        public RelayCommand LoadCoins { get; set; }

        private BindingList<Coin> _coins;
        public BindingList<Coin> Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                OnPropertyChanged();
            }
        }

        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                _selectedCoin = value;
                OnPropertyChanged();
            }
        }

        public AddCoinViewModel()
        {
            AddCoinPanelViewModel = new AddCoinPanelViewModel();
            LoadCoins = new RelayCommand(async _ => Coins = await ApiService.LoadCoins());
        }
    }
}