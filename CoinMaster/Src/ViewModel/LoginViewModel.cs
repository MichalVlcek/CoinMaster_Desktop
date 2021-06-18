using System.Windows;
using System.Windows.Controls;
using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class LoginViewModel : AbstractAuthenticationViewModel
    {
        public LoginViewModel(
            IWindowManager windowManager,
            INavigationControllerAuthentication navigationController,
            IModelValidator<AbstractAuthenticationViewModel> validator)
            : base(windowManager, navigationController, validator)
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