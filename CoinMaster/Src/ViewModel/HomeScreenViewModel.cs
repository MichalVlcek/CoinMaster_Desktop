using System.Collections.Generic;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class HomeScreenViewModel : Screen
    {
        public List<Coin> Coins => TmpDatabase.Coins;
        
        public DashboardOverviewViewModel DashboardOverview { get; }
        
        public HomeScreenViewModel(DashboardOverviewViewModel dashboardOverviewViewModel)
        {
            DashboardOverview = dashboardOverviewViewModel;
        }
    }
}