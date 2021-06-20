using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CoinMaster.DB;
using CoinMaster.Events;
using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel.HomeScreen
{
    public class HomeScreenViewModel : Screen
    {
        private BindingList<Coin> _coins;
        public BindingList<Coin> Coins
        {
            get => _coins;
            set => SetAndNotify(ref _coins, value);
        }

        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                SetAndNotify(ref _selectedCoin, value);
                NotifyOfPropertyChange(() => EnableButtons);
                events.Publish(new ElementSelectedEvent<Coin> {Element = SelectedCoin});
            }
        }

        public bool EnableButtons => SelectedCoin is not null;

        public DashboardOverviewViewModel DashboardOverview { get; }

        private readonly INavigationControllerMain navigationController;
        private readonly IEventAggregator events;
        private readonly IWindowManager windowManager;
        private readonly CoinRepository coinRepository;

        public HomeScreenViewModel(
            CoinRepository coinRepository,
            INavigationControllerMain navigationController,
            IEventAggregator events,
            IWindowManager windowManager,
            DashboardOverviewViewModel dashboardOverviewViewModel)
        {
            this.coinRepository = coinRepository;
            this.navigationController = navigationController;
            this.events = events;
            this.windowManager = windowManager;
            DashboardOverview = dashboardOverviewViewModel;
        }

        public void NavigateToCoinDetail() => navigationController.NavigateToCoinDetail();

        protected override async void OnActivate()
        {
            base.OnActivate();

            try
            {
                await Task.Run(async () => Coins = new BindingList<Coin>(await coinRepository.LoadWatchedCoins()));
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something wrong happened, try again", "Unexpected error",
                    icon: MessageBoxImage.Error);
            }
        }

        public async Task DeleteCoin()
        {
            try
            {
                await coinRepository.DeleteCoin(SelectedCoin);
                await DashboardOverview.LoadData();
                Coins.Remove(SelectedCoin);
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something wrong happened, try again", "Unexpected error",
                    icon: MessageBoxImage.Error);
            }
        }
    }
}