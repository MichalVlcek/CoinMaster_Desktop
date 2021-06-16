using System;
using System.ComponentModel;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionViewModel : AbstractCoinSubscriber, IHandle<TransactionsUpdatedEvent>
    {
        private readonly IEventAggregator eventAggregator;

        public TransactionEditViewModel TransactionEdit { get; }

        private BindingList<Transaction> _transactions;
        public BindingList<Transaction> Transactions
        {
            get => _transactions;
            set
            {
                SetAndNotify(ref _transactions, value);
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                SetAndNotify(ref _selectedTransaction, value);
                NotifyOfPropertyChange(() => CanDelete);
                eventAggregator.Publish(new ElementSelectedEvent<Transaction> {Element = _selectedTransaction});
            }
        }

        public bool CanDelete =>
            SelectedTransaction is not null && !Transaction.IsEmptyTransaction(SelectedTransaction);

        public TransactionViewModel(IEventAggregator eventAggregator, TransactionEditViewModel transactionEdit)
            : base(eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            TransactionEdit = transactionEdit;

            TmpDatabase.Transactions.Add(
                new Transaction
                {
                    Type = TransactionType.BUY,
                    Date = DateTime.Now,
                    CoinId = "bitcoin",
                    Amount = 0.125m,
                    CoinPrice = 52000,
                    Fee = 2,
                    Description = "ajaoa"
                });
            TmpDatabase.Transactions.Add(
                new Transaction
                {
                    Type = TransactionType.BUY,
                    Date = DateTime.Now,
                    CoinId = "bitcoin",
                    Amount = 0.125m,
                    CoinPrice = 52000,
                    Fee = 2,
                    Description = "ajaoa"
                });

            Transactions = new BindingList<Transaction>(TmpDatabase.Transactions);
        }

        public void AddNewTransaction() => SelectedTransaction = Transaction.EmptyTransaction;

        public void DeleteTransactions() => Transactions.Remove(SelectedTransaction);

        public void Handle(TransactionsUpdatedEvent message)
        {
            Transactions = new BindingList<Transaction>(message.Transactions);
        }
    }
}