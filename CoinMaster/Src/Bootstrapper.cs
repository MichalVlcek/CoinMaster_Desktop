using CoinMaster.Core;
using CoinMaster.Data;
using CoinMaster.DB;
using CoinMaster.Model;
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

            builder.Bind<CoinDataContext>().ToSelf().InSingletonScope();
            builder.Bind<CoinRepository>().ToSelf().InSingletonScope();
            builder.Bind<TransactionRepository>().ToSelf().InSingletonScope();
        }

        protected override void OnLaunch()
        {
            var navigationController = Container.Get<NavigationController>();
            navigationController.Delegate = RootViewModel;
            navigationController.NavigateToHomeScreen();

            // Creating instances of the objects on launch, because I need the objects to be already created when starting app
            var coinOverview = Container.Get<CoinOverviewViewModel>();
            var coinTitle = Container.Get<CoinDetailTitleViewModel>();
            var transactionView = Container.Get<TransactionViewModel>();
            var dataContext = Container.Get<CoinDataContext>();
            var coinRepository = Container.Get<CoinRepository>();
            var transactionRepository = Container.Get<TransactionRepository>();
        }
    }
}