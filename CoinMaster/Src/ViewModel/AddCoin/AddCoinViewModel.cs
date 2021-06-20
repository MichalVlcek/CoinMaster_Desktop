using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CoinMaster.DB;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel.AddCoin
{
    public class AddCoinViewModel : Screen
    {
        public AddCoinPanelViewModel AddCoinPanel { get; set; }

        private readonly IEventAggregator events;
        private readonly IWindowManager windowManager;

        private readonly CoinRepository coinRepository;

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

        public AddCoinViewModel(
            CoinRepository coinRepository,
            AddCoinPanelViewModel addCoinPanel,
            IEventAggregator events,
            IWindowManager windowManager
        )
        {
            this.events = events;
            this.windowManager = windowManager;
            this.coinRepository = coinRepository;
            AddCoinPanel = addCoinPanel;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            try
            {
                await Task.Run(async () => { Coins = new BindingList<Coin>(await coinRepository.LoadAllCoins()); });
            }
            catch (ApplicationException e)
            {
                windowManager.ShowMessageBox(e.Message, "Coin Loading Failed", icon: MessageBoxImage.Error);
            }
        }
    }
}