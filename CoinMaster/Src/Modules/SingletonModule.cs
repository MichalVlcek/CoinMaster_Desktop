using CoinMaster.ViewModel;
using StyletIoC;

namespace CoinMaster.Modules
{
    public class SingletonModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<CoinOverviewViewModel>().ToSelf().InSingletonScope();
            Bind<CoinDetailTitleViewModel>().ToSelf().InSingletonScope();
            Bind<TransactionViewModel>().ToSelf().InSingletonScope();
        }
    }
}