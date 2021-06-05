using CoinMaster.Core;
using CoinMaster.Model;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : ObservableObject
    {
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

        public AddCoinPanelViewModel()
        {
        }
    }
}