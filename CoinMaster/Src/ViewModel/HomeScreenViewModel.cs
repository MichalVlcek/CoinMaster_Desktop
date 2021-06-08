using System.Collections.Generic;
using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class HomeScreenViewModel : Screen
    {
        public List<Coin> Coins => TmpDatabase.Coins;

        public DashboardOverviewViewModel DashboardOverview { get; }

        private readonly INavigationController navigationController;

        public HomeScreenViewModel(INavigationController navigationController,
            DashboardOverviewViewModel dashboardOverviewViewModel)
        {
            this.navigationController = navigationController;
            DashboardOverview = dashboardOverviewViewModel;
        }

        public void NavigateToCoinOverview() => navigationController.NavigateToCoinOverview();
    }
}