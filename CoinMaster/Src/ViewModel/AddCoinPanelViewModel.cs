using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : Screen
    {
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            set => SetAndNotify(ref _selectedCoin, value);
        }
    }
}