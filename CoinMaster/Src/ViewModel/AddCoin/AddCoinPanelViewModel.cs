using System.Threading.Tasks;
using CoinMaster.DB;
using Stylet;

namespace CoinMaster.ViewModel.AddCoin
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
        }
    }
}