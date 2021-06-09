using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class CoinDetailViewModel : Screen
    {
        public CoinOverviewViewModel CoinOverview { get; }

        private readonly INavigationController navigationController;

        private IScreen _selectedScreen;
        public IScreen SelectedScreen
        {
            get => _selectedScreen;
            set => SetAndNotify(ref _selectedScreen, value);
        }

        public CoinDetailViewModel(INavigationController navigationController, CoinOverviewViewModel coinOverview)
        {
            CoinOverview = coinOverview;
            SelectedScreen = coinOverview;
            this.navigationController = navigationController;
        }

        public void ShowOverview() => SelectedScreen = CoinOverview;
        public void ShowTransactions() => SelectedScreen = CoinOverview;

        public void NavigateToHomeScreen() => navigationController.NavigateToHomeScreen();
    }
}