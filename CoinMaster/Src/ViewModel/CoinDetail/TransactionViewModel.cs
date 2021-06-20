using System.ComponentModel;
using System.Threading.Tasks;
using CoinMaster.DB;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel.CoinDetail
{
    public class TransactionViewModel : AbstractCoinSubscriber, IHandle<TransactionsUpdatedEvent>
    {
        private readonly IEventAggregator eventAggregator;

        private readonly TransactionRepository transactionRepository;
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

        public TransactionViewModel(
            TransactionRepository transactionRepository,
            IEventAggregator eventAggregator,
            TransactionEditViewModel transactionEdit)
            : base(eventAggregator)
        {
            this.transactionRepository = transactionRepository;
            this.eventAggregator = eventAggregator;
            TransactionEdit = transactionEdit;
        }

        public void AddNewTransaction() =>
            SelectedTransaction = Transaction.EmptyTransaction(SelectedCoin, LoggedUser.User);

        public async void DeleteTransactions()
        {
            await transactionRepository.DeleteTransaction(SelectedTransaction);
            Transactions.Remove(SelectedTransaction);
        }

        public async void Handle(TransactionsUpdatedEvent message)
        {
            if (Transactions.Contains(SelectedTransaction))
            {
                await transactionRepository.UpdateTransaction(message.Transaction);
            }
            else
            {
                await transactionRepository.InsertTransaction(message.Transaction);
            }

            await LoadTransactions();
            SelectedTransaction = null;
        }

        protected override async void OnViewLoaded()
        {
            base.OnViewLoaded();
            SelectedTransaction = null;
            await LoadTransactions();
        }

        private async Task LoadTransactions() =>
            Transactions =
                new BindingList<Transaction>(await transactionRepository.GetTransactionsForCoin(SelectedCoin));
    }
}