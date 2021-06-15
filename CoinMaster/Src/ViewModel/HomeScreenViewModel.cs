using System.Collections.Generic;
using CoinMaster.Events;
using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class HomeScreenViewModel : Screen
    {
        public List<Coin> Coins => TmpDatabase.Coins;

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

        public HomeScreenViewModel(INavigationController navigationController, IEventAggregator events,
            DashboardOverviewViewModel dashboardOverviewViewModel)
        {
            this.navigationController = navigationController;
            this.events = events;
            DashboardOverview = dashboardOverviewViewModel;
        }

        public void NavigateToCoinDetail() => navigationController.NavigateToCoinDetail();
    }
}