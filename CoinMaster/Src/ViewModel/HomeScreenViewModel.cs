using System.ComponentModel;
using System.Threading.Tasks;
using CoinMaster.Data;
using CoinMaster.Events;
using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
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
                events.Publish(new ElementSelectedEvent<Coin> {Element = SelectedCoin});
            }
        }

        public DashboardOverviewViewModel DashboardOverview { get; }

        private readonly INavigationController navigationController;
        private readonly IEventAggregator events;
        private readonly CoinRepository coinRepository;

        public HomeScreenViewModel(
            CoinRepository coinRepository,
            INavigationController navigationController,
            IEventAggregator events,
            DashboardOverviewViewModel dashboardOverviewViewModel)
        {
            this.coinRepository = coinRepository;
            this.navigationController = navigationController;
            this.events = events;
            DashboardOverview = dashboardOverviewViewModel;
        }

        public void NavigateToCoinDetail() => navigationController.NavigateToCoinDetail();

        protected override async void OnActivate()
        {
            base.OnActivate();

            await Task.Run(async () => Coins = new BindingList<Coin>(await coinRepository.LoadWatchedCoins()));
        }
    }
}