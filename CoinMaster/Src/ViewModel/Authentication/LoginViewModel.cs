using System.Windows;
using System.Windows.Controls;
using CoinMaster.DB;
using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel.Authentication
{
    public class LoginViewModel : AbstractAuthenticationViewModel
    {
        public LoginViewModel(
            IWindowManager windowManager,
            INavigationControllerAuthentication navigationController,
            IModelValidator<AbstractAuthenticationViewModel> validator,
            UserRepository userRepository)
            : base(windowManager, navigationController, validator, userRepository)
        {
        }

        public void Login(PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
            if (Password == string.Empty)
            {
                WindowManager.ShowMessageBox("Password can't be empty", "Empty Password", icon: MessageBoxImage.Error);
                return;;
            }

            NavigateToMain();
        }

        public void NavigateToMain() => NavigationController.NavigateToMain();
        public void NavigateToRegister() => NavigationController.NavigateToRegister();
    }
}