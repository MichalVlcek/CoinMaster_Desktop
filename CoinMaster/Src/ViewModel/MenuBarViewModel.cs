using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class MenuBarViewModel : Screen
    {
        private readonly INavigationControllerMain navigationController;
        private readonly INavigationControllerAuthentication navigationControllerAuth;

        public MenuBarViewModel(
            INavigationControllerMain navigationController,
            INavigationControllerAuthentication navigationControllerAuth)
        {
            this.navigationController = navigationController;
            this.navigationControllerAuth = navigationControllerAuth;
        }

        public void SignOut()
        {
            LoggedUser.User = null;
            NavigateToLogin();
        }
        
        public void NavigateToAddCoinScreen() => navigationController.NavigateToAddCoinsScreen();
        public void NavigateToHomeScreen() => navigationController.NavigateToHomeScreen();
        public void NavigateToLogin() => navigationControllerAuth.NavigateToLogin();
    }
}