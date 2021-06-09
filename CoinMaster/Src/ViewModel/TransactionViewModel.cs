using System;
using System.ComponentModel;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionViewModel : AbstractCoinSubscriber
    {
        private BindingList<Transaction> _transactions;

        public BindingList<Transaction> Transactions
        {
            get => _transactions;
            set => SetAndNotify(ref _transactions, value);
        }

        private Transaction _selectedTransaction;

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetAndNotify(ref _selectedTransaction, value);
        }
        
        public TransactionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Transactions = new BindingList<Transaction>()
            {
                new Transaction
                {
                    Type = TransactionType.BUY,
                    Date = DateTime.Now.Date,
                    CoinId = "bitcoin",
                    Amount = 0.125m,
                    Cost = 520,
                    Fee = 2,
                    Description = "ajaoa"
                }
            };
        }
    }
}