using System;
using System.Collections.Generic;
using System.Windows;
using CoinMaster.DB;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel.CoinDetail
{
    public class CoinOverviewViewModel : AbstractCoinSubscriber
    {
        private readonly IWindowManager windowManager;

        private readonly TransactionRepository transactionRepository;

        private List<Transaction> Transactions { get; set; }

        public string CoinHoldings =>
            StringFormats.CurrencyFormat(CoinUtils.CountHoldings(Transactions), SelectedCoin.Symbol);

        public string HoldingsValue =>
            StringFormats.CurrencyFormat(CoinUtils.CountHoldingsValue(Transactions, SelectedCoin.Price));

        public string ProfitLoss =>
            StringFormats.CurrencyFormat(CoinUtils.CountProfitOrLoss(Transactions, SelectedCoin.Price));

        public string AverageCost => StringFormats.CurrencyFormat(CoinUtils.CountAverageCost(Transactions));
        public string Deposit => StringFormats.CurrencyFormat(CoinUtils.CountTotalCost(Transactions));

        public string PercentChange =>
            StringFormats.PercentFormat(CoinUtils.CountPercentChange(Transactions, SelectedCoin.Price));

        public CoinOverviewViewModel(
            TransactionRepository transactionRepository,
            IEventAggregator eventAggregator,
            IWindowManager windowManager) :
            base(eventAggregator)
        {
            this.transactionRepository = transactionRepository;
            this.windowManager = windowManager;
        }

        protected override async void OnViewLoaded()
        {
            base.OnViewLoaded();

            try
            {
                Transactions = await transactionRepository.GetTransactionsForCoin(SelectedCoin);
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something wrong happened, try again", "Unexpected error",
                    icon: MessageBoxImage.Error);
            }

            NotifyOfPropertyChange(() => CoinHoldings);
            NotifyOfPropertyChange(() => HoldingsValue);
            NotifyOfPropertyChange(() => ProfitLoss);
            NotifyOfPropertyChange(() => AverageCost);
            NotifyOfPropertyChange(() => Deposit);
            NotifyOfPropertyChange(() => PercentChange);
        }
    }
}