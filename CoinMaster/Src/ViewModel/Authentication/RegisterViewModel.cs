using System;
using System.Windows;
using System.Windows.Controls;
using CoinMaster.DB;
using CoinMaster.Exceptions;
using CoinMaster.Interfaces;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel.Authentication
{
    public class RegisterViewModel : AbstractAuthenticationViewModel
    {

        public RegisterViewModel(
            IWindowManager windowManager, 
            INavigationControllerAuthentication navigationController, 
            IModelValidator<AbstractAuthenticationViewModel> validator,
            UserRepository userRepository) 
            : base(windowManager, navigationController, validator, userRepository)
        {
        }

        public async void Register(PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
            if (Password == string.Empty)
            {
                WindowManager.ShowMessageBox("Password can't be empty", "Empty Password", icon: MessageBoxImage.Error);
                return;
            }

            try
            {
                var user = await UserRepository.RegisterUser(Email, Password);
                LoggedUser.User = user;
                NavigateToMain();
            }
            catch (UserExistsException e)
            {
                WindowManager.ShowMessageBox(e.Message, "User Exists", icon: MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                WindowManager.ShowMessageBox("Something bad happened, try again.", "Error", icon: MessageBoxImage.Error);
            }
        }
        
        public void NavigateToMain() => NavigationController.NavigateToMain();
        public void NavigateToLogin() => NavigationController.NavigateToLogin();
    }
}