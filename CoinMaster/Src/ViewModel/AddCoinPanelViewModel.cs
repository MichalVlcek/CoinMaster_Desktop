using System.Threading.Tasks;
using CoinMaster.Model;
using CoinMaster.DB;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        private CoinDataContext dataContext;

        public AddCoinPanelViewModel(CoinDataContext dataContext, IEventAggregator eventAggregator) :
            base(eventAggregator)
        {
            this.dataContext = dataContext;
        }

        public async Task AddCoin()
        {
            await Task.Run(async () =>
            {
                await dataContext.AddAsync(SelectedCoin);
                await dataContext.SaveChangesAsync();
            });

            TmpDatabase.Coins.Add(SelectedCoin);
        }
    }
}