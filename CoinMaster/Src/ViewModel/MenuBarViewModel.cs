﻿using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class MenuBarViewModel : Screen
    {
        private readonly INavigationController navigationController;

        public MenuBarViewModel(INavigationController navigationController)
        {
            this.navigationController = navigationController;
        }

        public void NavigateToAddCoinScreen() => navigationController.NavigateToAddCoinsScreen();
        public void NavigateToHomeScreen() => navigationController.NavigateToHomeScreen();
    }
}