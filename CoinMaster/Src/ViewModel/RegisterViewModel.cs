using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class RegisterViewModel : Screen
    {
        private readonly INavigationControllerAuthentication navigationController;

        public RegisterViewModel(
            INavigationControllerAuthentication navigationController)
        {
            this.navigationController = navigationController;
        }
        
        public void NavigateToMain() => navigationController.NavigateToMain();
        public void NavigateToLogin() => navigationController.NavigateToLogin();
    }
}