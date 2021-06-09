using CoinMaster.Core;
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
            
            builder.Bind<CoinOverviewViewModel>().ToSelf().InSingletonScope();
            builder.Bind<CoinDetailTitleViewModel>().ToSelf().InSingletonScope();
        }

        protected override void OnLaunch()
        {
            var navigationController = Container.Get<NavigationController>();
            navigationController.Delegate = RootViewModel;
            navigationController.NavigateToHomeScreen();

            // Creating instances of the objects on launch, because I need the objects to be already created when starting app
            var coinOverview = Container.Get<CoinOverviewViewModel>();
            var coinTitle = Container.Get<CoinDetailTitleViewModel>();
        }
    }
}