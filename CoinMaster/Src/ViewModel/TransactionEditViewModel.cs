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
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            private set => SetAndNotify(ref _selectedCoin, value);
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
        public string CoinPriceText
        {
            get => _coinPrice;
            set => SetAndNotify(ref _coinPrice, value);
        }

        private string _amount;
        public string AmountText
        {
            get => _amount;
            set => SetAndNotify(ref _amount, value);
        }

        private string _fee;
        public string FeeText
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
            CoinPriceText = SelectedTransaction.CoinPrice.ToString(CultureInfo.InvariantCulture);
            AmountText = SelectedTransaction.Amount.ToString(CultureInfo.InvariantCulture);
            FeeText = SelectedTransaction.Fee.ToString(CultureInfo.InvariantCulture);
            DateText = SelectedTransaction.Date;
            DescriptionText = SelectedTransaction.Description;

            Validate();
        }
        
        public void Handle(ElementSelectedEvent<Coin> message)
        {
            SelectedCoin = message.Element;
        }

        public void UpdateTransaction()
        {
            SelectedTransaction.Type = SelectedType;
            SelectedTransaction.CoinPrice = Convert.ToDecimal(CoinPriceText, CultureInfo.InvariantCulture);
            SelectedTransaction.Amount = Convert.ToDecimal(AmountText, CultureInfo.InvariantCulture);
            SelectedTransaction.Fee = Convert.ToDecimal(FeeText, CultureInfo.InvariantCulture);
            SelectedTransaction.Date = DateText;
            SelectedTransaction.Description = DescriptionText;
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