using System.Windows;
using System.Windows.Controls;
using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel.Authentication
{
    public class RegisterViewModel : AbstractAuthenticationViewModel
    {

        public RegisterViewModel(
            IWindowManager windowManager, 
            INavigationControllerAuthentication navigationController, 
            IModelValidator<AbstractAuthenticationViewModel> validator) 
            : base(windowManager, navigationController, validator)
        {
        }

        public void Register(PasswordBox passwordBox)
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
        public void NavigateToLogin() => NavigationController.NavigateToLogin();
    }
}