using System;
using System.ComponentModel;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionViewModel : AbstractCoinSubscriber
    {
        private IEventAggregator eventAggregator;

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
            set
            {
                SetAndNotify(ref _selectedTransaction, value);
                eventAggregator.Publish(new ElementSelectedEvent<Transaction> {Element = _selectedTransaction});
            }
        }

        public TransactionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Transactions = new BindingList<Transaction>()
            {
                new Transaction
                {
                    Type = TransactionType.BUY,
                    Date = DateTime.Now,
                    CoinId = "bitcoin",
                    Amount = 0.125m,
                    CoinPrice = 52000,
                    Fee = 2,
                    Description = "ajaoa"
                }
            };
        }
    }
}