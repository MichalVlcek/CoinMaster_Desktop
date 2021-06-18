using System;
using CoinMaster.Core;
using CoinMaster.Interfaces;
using CoinMaster.ViewModel;
using StyletIoC;

namespace CoinMaster.Modules
{
    public class NavigationModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<NavigationControllerMain>().And<INavigationControllerMain>()
                .To<NavigationControllerMain>().InSingletonScope();
            Bind<NavigationControllerAuthentication>().And<INavigationControllerAuthentication>()
                .To<NavigationControllerAuthentication>().InSingletonScope();

            Bind<Func<AddCoinViewModel>>()
                .ToFactory<Func<AddCoinViewModel>>(c => () => c.Get<AddCoinViewModel>());
            Bind<Func<HomeScreenViewModel>>()
                .ToFactory<Func<HomeScreenViewModel>>(c => () => c.Get<HomeScreenViewModel>());
            Bind<Func<CoinDetailViewModel>>()
                .ToFactory<Func<CoinDetailViewModel>>(c => () => c.Get<CoinDetailViewModel>());

            Bind<Func<LoginViewModel>>()
                .ToFactory<Func<LoginViewModel>>(c => () => c.Get<LoginViewModel>());
            Bind<Func<RegisterViewModel>>()
                .ToFactory<Func<RegisterViewModel>>(c => () => c.Get<RegisterViewModel>());
            Bind<Func<MainContentViewModel>>()
                .ToFactory<Func<MainContentViewModel>>(c => () => c.Get<MainContentViewModel>());
        }
    }
}