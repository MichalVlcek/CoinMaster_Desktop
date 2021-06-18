using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class LoginViewModel : Screen
    {
        private readonly INavigationControllerAuthentication navigationController;

        public LoginViewModel(
            INavigationControllerAuthentication navigationController)
        {
            this.navigationController = navigationController;
        }
        
        public void NavigateToMain() => navigationController.NavigateToMain();
        public void NavigateToRegister() => navigationController.NavigateToRegister();
    }
}