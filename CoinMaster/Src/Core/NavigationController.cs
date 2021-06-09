﻿using System;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel;

namespace CoinMaster.Core
{
    public class NavigationController : INavigationController
    {
        private readonly Func<AddCoinViewModel> addCoinViewModelFactory;
        private readonly Func<HomeScreenViewModel> homeScreenViewModelFactory;
        private readonly Func<CoinDetailViewModel> coinDetailViewModelFactory;

        public INavigationControllerDelegate Delegate { get; set; }

        public NavigationController(
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