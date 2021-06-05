using System;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel;

namespace CoinMaster.Core
{
    public class NavigationController : INavigationController
    {
        private readonly Func<AddCoinViewModel> addCoinViewModelFactory;
        private readonly Func<HomeScreenViewModel> homeScreenViewModelFactory;

        public INavigationControllerDelegate Delegate { get; set; }
        
        public NavigationController(Func<AddCoinViewModel> addCoinViewModelFactory,
            Func<HomeScreenViewModel> homeScreenViewModelFactory)
        {
            this.addCoinViewModelFactory = addCoinViewModelFactory;
            this.homeScreenViewModelFactory = homeScreenViewModelFactory;
        }


        public void NavigateToAddCoinsScreen()
        {
            Delegate?.NavigateTo(addCoinViewModelFactory());
        }

        public void NavigateToHomeScreen()
        {
            Delegate?.NavigateTo(homeScreenViewModelFactory());

        }
    }
}