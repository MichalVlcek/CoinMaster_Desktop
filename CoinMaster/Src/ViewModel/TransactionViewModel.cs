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

        public TransactionEditViewModel TransactionEdit { get; }

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

        public TransactionViewModel(IEventAggregator eventAggregator, TransactionEditViewModel transactionEdit)
            : base(eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            TransactionEdit = transactionEdit;

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
                },
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

        public void AddNewTransaction() => SelectedTransaction = Transaction.EmptyTransaction;

        public void DeleteTransactions() => Transactions.Remove(SelectedTransaction);
    }
}