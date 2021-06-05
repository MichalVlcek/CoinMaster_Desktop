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
            Bind<NavigationController>().And<INavigationController>().To<NavigationController>().InSingletonScope();
            Bind<Func<AddCoinViewModel>>().ToFactory<Func<AddCoinViewModel>>(c => () => c.Get<AddCoinViewModel>());
            Bind<Func<HomeScreenViewModel>>().ToFactory<Func<HomeScreenViewModel>>(c => () => c.Get<HomeScreenViewModel>());
        }
    }
}