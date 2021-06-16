using System;
using System.ComponentModel;
using System.Linq;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionEditViewModel : AbstractCoinSubscriber, IHandle<ElementSelectedEvent<Transaction>>
    {
        private readonly IEventAggregator eventAggregator;

        private Transaction _selectedTransaction;
        private Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetAndNotify(ref _selectedTransaction, value);
        }

        public BindingList<TransactionType> TransactionTypes { get; } =
            new(Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList());

        private TransactionType _selectedType;
        public TransactionType SelectedType
        {
            get => _selectedType;
            set => SetAndNotify(ref _selectedType, value);
        }

        private decimal _coinPrice;
        public decimal CoinPriceText
        {
            get => _coinPrice;
            set => SetAndNotify(ref _coinPrice, value);
        }

        private decimal _amount;
        public decimal AmountText
        {
            get => _amount;
            set => SetAndNotify(ref _amount, value);
        }

        private decimal _fee;
        public decimal FeeText
        {
            get => _fee;
            set => SetAndNotify(ref _fee, value);
        }

        private DateTime? _date;
        public DateTime DateText
        {
            get => _date ?? DateTime.Now;
            set => SetAndNotify(ref _date, value);
        }

        private string _description;
        public string DescriptionText
        {
            get => _description;
            set => SetAndNotify(ref _description, value);
        }

        public TransactionEditViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Handle(ElementSelectedEvent<Transaction> message)
        {
            SelectedTransaction = message.Element;
            SelectedType = SelectedTransaction.Type;
            CoinPriceText = SelectedTransaction.CoinPrice;
            AmountText = SelectedTransaction.Amount;
            FeeText = SelectedTransaction.Fee;
            DateText = SelectedTransaction.Date;
            DescriptionText = SelectedTransaction.Description;
        }

        public void UpdateTransaction()
        {
            SelectedTransaction.Type = SelectedType;
            SelectedTransaction.CoinPrice = CoinPriceText;
            SelectedTransaction.Amount = AmountText;
            SelectedTransaction.Fee = FeeText;
            SelectedTransaction.Date = DateText;
            SelectedTransaction.Description = DescriptionText;
            if (!TmpDatabase.Transactions.Contains(SelectedTransaction))
            {
                TmpDatabase.Transactions.Add(SelectedTransaction);
            }

            eventAggregator.Publish(new TransactionsUpdatedEvent {Transactions = TmpDatabase.Transactions});
        }
    }
}