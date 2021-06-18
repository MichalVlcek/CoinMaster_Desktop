using System;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel;

namespace CoinMaster.Core
{
    public class NavigationControllerAuthentication : INavigationControllerAuthentication
    {
        private readonly Func<LoginViewModel> loginFactory;
        private readonly Func<RegisterViewModel> registerFactory;
        private readonly Func<MainContentViewModel> mainFactory;

        public INavigationControllerDelegate Delegate { get; set; }

        public NavigationControllerAuthentication(
            Func<LoginViewModel> loginFactory,
            Func<RegisterViewModel> registerFactory, 
            Func<MainContentViewModel> mainFactory)
        {
            this.loginFactory = loginFactory;
            this.registerFactory = registerFactory;
            this.mainFactory = mainFactory;
        }

        public void NavigateToLogin() => Delegate?.NavigateTo(loginFactory());
        public void NavigateToRegister() => Delegate?.NavigateTo(registerFactory());
        public void NavigateToMain() => Delegate?.NavigateTo(mainFactory());
    }
}