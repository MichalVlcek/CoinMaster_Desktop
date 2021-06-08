using System;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel;

namespace CoinMaster.Core
{
    public class NavigationController : INavigationController
    {
        private readonly Func<AddCoinViewModel> addCoinViewModelFactory;
        private readonly Func<HomeScreenViewModel> homeScreenViewModelFactory;
        private readonly CoinOverviewViewModel coinOverviewViewModelFactory;

        public INavigationControllerDelegate Delegate { get; set; }

        public NavigationController(
            Func<AddCoinViewModel> addCoinViewModelFactory,
            Func<HomeScreenViewModel> homeScreenViewModelFactory,
           CoinOverviewViewModel coinOverviewViewModelFactory)
        {
            this.addCoinViewModelFactory = addCoinViewModelFactory;
            this.homeScreenViewModelFactory = homeScreenViewModelFactory;
            this.coinOverviewViewModelFactory = coinOverviewViewModelFactory;
        }

        public void NavigateToAddCoinsScreen() => Delegate?.NavigateTo(addCoinViewModelFactory());
        public void NavigateToHomeScreen() => Delegate?.NavigateTo(homeScreenViewModelFactory());
        public void NavigateToCoinOverview() => Delegate?.NavigateTo(coinOverviewViewModelFactory);
    }
}