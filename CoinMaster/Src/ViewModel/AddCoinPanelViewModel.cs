using CoinMaster.Model;
using CoinMaster.DB;
using CoinMaster.Events;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class AddCoinPanelViewModel : AbstractCoinSubscriber
    {
        public AddCoinPanelViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public void AddCoin()
        {
            TmpDatabase.Coins.Add(SelectedCoin);
        }

        public void SaveToDb()
        {
            // using var db = new Pes();
            // db.Add(SelectedCoin);
            // db.SaveChanges();
        }
    }
}