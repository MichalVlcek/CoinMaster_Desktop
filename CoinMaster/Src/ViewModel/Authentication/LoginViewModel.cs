using System;
using System.Windows;
using System.Windows.Controls;
using CoinMaster.DB;
using CoinMaster.Exceptions;
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

        public async void Login(PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
            if (Password == string.Empty)
            {
                WindowManager.ShowMessageBox("Password can't be empty", "Empty Password", icon: MessageBoxImage.Error);
                return;
            }

            try
            {
                await UserRepository.LoginUser(Email, Password);
                NavigateToMain();
            }
            catch (WrongCredentialsException e)
            {
                WindowManager.ShowMessageBox(e.Message, "Wrong Credentials", icon: MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                WindowManager.ShowMessageBox("Something bad happened, try again.", "Error", icon: MessageBoxImage.Error);
            }
        }

        public void NavigateToMain() => NavigationController.NavigateToMain();
        public void NavigateToRegister() => NavigationController.NavigateToRegister();
    }
}