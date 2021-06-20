using System;
using System.Threading.Tasks;
using System.Windows;
using CoinMaster.DB;
using Stylet;

namespace CoinMaster.ViewModel.AddCoin
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        private readonly CoinRepository coinRepository;
        private readonly IWindowManager windowManager;

        public AddCoinPanelViewModel(
            CoinRepository coinRepository,
            IEventAggregator eventAggregator,
            IWindowManager windowManager) :
            base(eventAggregator)
        {
            this.coinRepository = coinRepository;
            this.windowManager = windowManager;
        }

        public async Task AddCoin()
        {
            try
            {
                await coinRepository.InsertCoin(SelectedCoin);
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something wrong happened, try again", "Unexpected error",
                    icon: MessageBoxImage.Error);
            }
        }
    }
}