using System.Threading.Tasks;
using CoinMaster.Data;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        private CoinRepository coinRepository;
        
        public AddCoinPanelViewModel(CoinRepository coinRepository, IEventAggregator eventAggregator) :
            base(eventAggregator)
        {
            this.coinRepository = coinRepository;
        }

        public async Task AddCoin()
        {
            await coinRepository.InsertCoin(SelectedCoin);
            TmpDatabase.Coins.Add(SelectedCoin);
        }
    }
}