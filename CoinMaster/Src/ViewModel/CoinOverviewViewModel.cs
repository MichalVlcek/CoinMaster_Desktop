using System.Collections.Generic;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class CoinOverviewViewModel : AbstractCoinSubscriber
    {
        private List<Transaction> Transactions => TmpDatabase.Transactions;

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

        public CoinOverviewViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            
            NotifyOfPropertyChange(() => CoinHoldings);
            NotifyOfPropertyChange(() => HoldingsValue);
            NotifyOfPropertyChange(() => ProfitLoss);
            NotifyOfPropertyChange(() => AverageCost);
            NotifyOfPropertyChange(() => Deposit);
            NotifyOfPropertyChange(() => PercentChange);
        }
    }
}