using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class CoinDetailViewModel : Screen
    {
        public CoinDetailTitleViewModel CoinTitle { get; }
        private CoinOverviewViewModel CoinOverview { get; }
        private TransactionViewModel TransactionViewModel { get; }

        private readonly INavigationController navigationController;

        private IScreen _selectedScreen;

        public IScreen SelectedScreen
        {
            get => _selectedScreen;
            set => SetAndNotify(ref _selectedScreen, value);
        }

        public CoinDetailViewModel(INavigationController navigationController, 
            CoinDetailTitleViewModel coinTitle,
            CoinOverviewViewModel coinOverview,
            TransactionViewModel transactionScreen)
        {
            CoinTitle = coinTitle;
            CoinOverview = coinOverview;
            TransactionViewModel = transactionScreen;
            SelectedScreen = coinOverview;
            this.navigationController = navigationController;
        }

        public void ShowOverview() => SelectedScreen = CoinOverview;
        public void ShowTransactions() => SelectedScreen = TransactionViewModel;

        public void NavigateToHomeScreen() => navigationController.NavigateToHomeScreen();
    }
}