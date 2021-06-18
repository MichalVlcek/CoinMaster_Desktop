using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMaster.Data;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel.HomeScreen
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

        public string TotalSpent =>
            StringFormats.CurrencyFormat(CoinUtils.CountTotalCost(Transactions));

        public string ProfitLoss =>
            StringFormats.CurrencyFormat(CoinUtils.CountProfitOrLoss(Transactions, Coins));

        public string PercentChange =>
            StringFormats.PercentFormat(CoinUtils.CountPercentChange(Transactions, Coins));

        public async Task LoadData()
        {
            await Task.Run(async () =>
            {
                Transactions = await transactionRepository.GetTransactionsAll();
                Coins = await coinRepository.GetAllFromDatabase();
            });
            
            Refresh();
        }
        
        protected override async void OnViewLoaded()
        {
            base.OnViewLoaded();

            await LoadData();
        }
    }
}