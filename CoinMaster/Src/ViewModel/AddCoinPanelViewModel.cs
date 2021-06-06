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
            private set
            {
                SetAndNotify(ref _selectedCoin, value); 
                OnPropertyChanged(nameof(Rank));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Ath));
                OnPropertyChanged(nameof(Atl));
                OnPropertyChanged(nameof(AthChange));
                OnPropertyChanged(nameof(AtlChange));
                OnPropertyChanged(nameof(MarketCap));
                OnPropertyChanged(nameof(CirculatingSupply));
                OnPropertyChanged(nameof(MaximumSupply));
                OnPropertyChanged(nameof(PriceChangePercent24H));
                OnPropertyChanged(nameof(PriceChangePercent7D));
                OnPropertyChanged(nameof(PriceChange24H));
                OnPropertyChanged(nameof(PriceChange7D));
            }
        }

        public string Rank => StringFormats.RankFormat(SelectedCoin.Rank);
        public string Price => StringFormats.CurrencyFormat(SelectedCoin.Price);
        public string Ath => StringFormats.CurrencyFormat(SelectedCoin.Ath, SelectedCoin.Symbol);
        public string Atl => StringFormats.CurrencyFormat(SelectedCoin.Atl, SelectedCoin.Symbol);
        public string AthChange => StringFormats.PercentFormat(SelectedCoin.AthPercentChange);
        public string AtlChange => StringFormats.PercentFormat(SelectedCoin.AtlPercentChange);
        public string MarketCap => StringFormats.CurrencyFormat(SelectedCoin.MarketCap, SelectedCoin.Symbol);

        public string CirculatingSupply =>
            StringFormats.CurrencyFormat(SelectedCoin.CirculatingSupply, SelectedCoin.Symbol);

        public string MaximumSupply => StringFormats.CurrencyFormat(SelectedCoin.MaxSupply ?? 0, SelectedCoin.Symbol);
        public string PriceChangePercent24H => StringFormats.PercentFormat(SelectedCoin.PriceChangePercent24H ?? 0);
        public string PriceChangePercent7D => StringFormats.PercentFormat(SelectedCoin.PriceChangePercent7D ?? 0);

        public string PriceChange24H =>
            StringFormats.CurrencyFormat(CoinUtils.CalculatePriceChange(SelectedCoin.Price,
                SelectedCoin.PriceChangePercent24H ?? 0));

        public string PriceChange7D =>
            StringFormats.CurrencyFormat(CoinUtils.CalculatePriceChange(SelectedCoin.Price,
                SelectedCoin.PriceChangePercent7D ?? 0));

        public AddCoinPanelViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }
        
        public void Handle(CoinSelectedEvent message)
        {
            SelectedCoin = message.Coin;
        }
    }
}