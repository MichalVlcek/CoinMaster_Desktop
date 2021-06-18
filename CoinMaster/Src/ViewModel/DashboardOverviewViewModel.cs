using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMaster.Data;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class DashboardOverviewViewModel : Screen
    {
        private readonly CoinRepository coinRepository;
        private readonly TransactionRepository transactionRepository;
        
        private List<Transaction> Transactions { get; set; }
        private List<Coin> Coins { get; set; }

        public DashboardOverviewViewModel(CoinRepository coinRepository, TransactionRepository transactionRepository)
        {
            this.coinRepository = coinRepository;
            this.transactionRepository = transactionRepository;
        }

        public string HoldingsValue =>
            StringFormats.CurrencyFormat(CoinUtils.CountHoldingsValue(Coins));

        protected override async void OnViewLoaded()
        {
            base.OnViewLoaded();
            
            // Transactions = await transactionRepository.GetTransactionsAll();
            await Task.Run( async () => Coins = await coinRepository.GetAllFromDatabase());
            NotifyOfPropertyChange(() => HoldingsValue);
        }
    }
}