using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionEditViewModel : AbstractCoinSubscriber, IHandle<ElementSelectedEvent<Transaction>>
    {
        private Transaction _selectedTransaction;

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetAndNotify(ref _selectedTransaction, value);
        }
        
        public TransactionEditViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public void Handle(ElementSelectedEvent<Transaction> message)
        {
            SelectedTransaction = message.Element;
        }
    }
}