using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using CoinMaster.DB;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel.HomeScreen
{
    public class DashboardOverviewViewModel : Screen
    {
        private readonly IWindowManager windowManager;
        private readonly CoinRepository coinRepository;
        private readonly TransactionRepository transactionRepository;

        private List<Transaction> Transactions { get; set; }
        private List<Coin> Coins { get; set; }

        public DashboardOverviewViewModel(
            CoinRepository coinRepository,
            TransactionRepository transactionRepository,
            IWindowManager windowManager)
        {
            this.coinRepository = coinRepository;
            this.transactionRepository = transactionRepository;
            this.windowManager = windowManager;
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
            try
            {
                await Task.Run(async () =>
                {
                    Transactions = await transactionRepository.GetTransactionsAll();
                    Coins = await coinRepository.GetAllFromDatabase();
                });
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something wrong happened, try again", "Unexpected error",
                    icon: MessageBoxImage.Error);
            }

            Refresh();
        }

        protected override async void OnViewLoaded()
        {
            base.OnViewLoaded();

            await LoadData();
        }
    }
}