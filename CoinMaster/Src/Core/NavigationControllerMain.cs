using System;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel.AddCoin;
using CoinMaster.ViewModel.CoinDetail;
using CoinMaster.ViewModel.HomeScreen;

namespace CoinMaster.Core
{
    public class NavigationControllerMain : INavigationControllerMain
    {
        private readonly Func<AddCoinViewModel> addCoinViewModelFactory;
        private readonly Func<HomeScreenViewModel> homeScreenViewModelFactory;
        private readonly Func<CoinDetailViewModel> coinDetailViewModelFactory;

        public INavigationControllerDelegate Delegate { get; set; }

        public NavigationControllerMain(
            Func<AddCoinViewModel> addCoinViewModelFactory,
            Func<HomeScreenViewModel> homeScreenViewModelFactory,
            Func<CoinDetailViewModel> coinDetailViewModelFactory)
        {
            this.addCoinViewModelFactory = addCoinViewModelFactory;
            this.homeScreenViewModelFactory = homeScreenViewModelFactory;
            this.coinDetailViewModelFactory = coinDetailViewModelFactory;
        }

        public void NavigateToAddCoinsScreen() => Delegate?.NavigateTo(addCoinViewModelFactory());
        public void NavigateToHomeScreen() => Delegate?.NavigateTo(homeScreenViewModelFactory());
        public void NavigateToCoinDetail() => Delegate?.NavigateTo(coinDetailViewModelFactory());
    }
}