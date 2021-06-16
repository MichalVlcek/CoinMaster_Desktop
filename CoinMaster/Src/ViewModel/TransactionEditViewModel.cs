using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CoinMaster.Events;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class TransactionEditViewModel : Screen,
        IHandle<ElementSelectedEvent<Coin>>,
        IHandle<ElementSelectedEvent<Transaction>>
    {
        private readonly IEventAggregator eventAggregator;
        private IWindowManager windowManager;

        private Transaction _selectedTransaction;
        private Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetAndNotify(ref _selectedTransaction, value);
        }

        private Coin _selectedCoin;
        private Coin SelectedCoin
        {
            get => _selectedCoin;
            set => SetAndNotify(ref _selectedCoin, value);
        }

        public BindingList<TransactionType> TransactionTypes { get; } =
            new(Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList());

        private TransactionType _selectedType;
        public TransactionType SelectedType
        {
            get => _selectedType;
            set => SetAndNotify(ref _selectedType, value);
        }

        private string _coinPrice;
        public string CoinPrice
        {
            get => _coinPrice;
            set => SetAndNotify(ref _coinPrice, value);
        }

        private string _amount;
        public string Amount
        {
            get => _amount;
            set => SetAndNotify(ref _amount, value);
        }

        private string _fee;
        public string Fee
        {
            get => _fee;
            set => SetAndNotify(ref _fee, value);
        }

        private DateTime? _date;
        public DateTime Date
        {
            get => _date ?? DateTime.Now;
            set => SetAndNotify(ref _date, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetAndNotify(ref _description, value);
        }

        public bool CanSubmit => !HasErrors || !AutoValidate;

        public TransactionEditViewModel(IWindowManager windowManager,
            IModelValidator<TransactionEditViewModel> validator, IEventAggregator eventAggregator) : base(validator)
        {
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;

            eventAggregator.Subscribe(this);
        }

        public void Handle(ElementSelectedEvent<Transaction> message)
        {
            SelectedTransaction = message.Element;
            SelectedType = SelectedTransaction.Type;
            CoinPrice = SelectedTransaction.CoinPrice.ToString(CultureInfo.InvariantCulture);
            Amount = SelectedTransaction.Amount.ToString(CultureInfo.InvariantCulture);
            Fee = SelectedTransaction.Fee.ToString(CultureInfo.InvariantCulture);
            Date = SelectedTransaction.Date;
            Description = SelectedTransaction.Description;

            Validate();
        }
        
        public void Handle(ElementSelectedEvent<Coin> message)
        {
            SelectedCoin = message.Element;
        }

        public void UpdateTransaction()
        {
            SelectedTransaction.Type = SelectedType;
            SelectedTransaction.CoinPrice = Convert.ToDecimal(CoinPrice, CultureInfo.InvariantCulture);
            SelectedTransaction.Amount = Convert.ToDecimal(Amount, CultureInfo.InvariantCulture);
            SelectedTransaction.Fee = Convert.ToDecimal(Fee, CultureInfo.InvariantCulture);
            SelectedTransaction.Date = Date;
            SelectedTransaction.Description = Description;
            if (!TmpDatabase.Transactions.Contains(SelectedTransaction))
            {
                TmpDatabase.Transactions.Add(SelectedTransaction);
            }

            eventAggregator.Publish(new TransactionsUpdatedEvent {Transactions = TmpDatabase.Transactions});
        }

        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);
            NotifyOfPropertyChange(() => CanSubmit);
        }
    }
}