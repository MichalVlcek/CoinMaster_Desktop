using System;
using CoinMaster.Core;
using CoinMaster.Data;
using CoinMaster.DB;
using CoinMaster.Modules;
using CoinMaster.ViewModel;
using Stylet;
using StyletIoC;

namespace CoinMaster
{
    public class Bootstrapper : Bootstrapper<RootViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.AddModule(new NavigationModule());
            builder.AddModule(new SingletonModule());
            builder.AddModule(new ValidationModule());

            builder.Bind<Func<CoinDataContext>>().ToFactory<Func<CoinDataContext>>(c => () => c.Get<CoinDataContext>());
            builder.Bind<CoinRepository>().ToSelf().InSingletonScope();
            builder.Bind<TransactionRepository>().ToSelf().InSingletonScope();
        }

        protected override void OnLaunch()
        {
            var navigationControllerMain = Container.Get<NavigationControllerMain>();
            navigationControllerMain.Delegate =  Container.Get<MainContentViewModel>();
            navigationControllerMain.NavigateToHomeScreen();

            var navigationControllerAuthentication = Container.Get<NavigationControllerAuthentication>();
            navigationControllerAuthentication.Delegate = RootViewModel;
            navigationControllerAuthentication.NavigateToLogin();

            // Creating instances of the objects on launch, because I need the objects to be already created when starting app
            var coinOverview = Container.Get<CoinOverviewViewModel>();
            var coinTitle = Container.Get<CoinDetailTitleViewModel>();
            var transactionView = Container.Get<TransactionViewModel>();
            var coinRepository = Container.Get<CoinRepository>();
            var transactionRepository = Container.Get<TransactionRepository>();
        }
    }
}