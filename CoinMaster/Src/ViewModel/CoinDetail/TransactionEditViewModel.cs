using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using CoinMaster.Events;
using CoinMaster.Model;
using CoinMaster.Utility;
using Stylet;

namespace CoinMaster.ViewModel.CoinDetail
{
    public class TransactionEditViewModel : Screen,
        IHandle<ElementSelectedEvent<Transaction>>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;

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

        public TransactionEditViewModel(
            IWindowManager windowManager,
            IModelValidator<TransactionEditViewModel> validator,
            IEventAggregator eventAggregator) : base(validator)
        {
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;

            eventAggregator.Subscribe(this);
        }

        public void Handle(ElementSelectedEvent<Transaction> message)
        {
            if (message.Element == null) return;
            
            SelectedTransaction = message.Element;
            SelectedType = SelectedTransaction.Type;
            CoinPrice = StringFormats.DecimalFormat(SelectedTransaction.CoinPrice);
            Amount = StringFormats.DecimalFormat(SelectedTransaction.Amount);
            Fee = StringFormats.DecimalFormat(SelectedTransaction.Fee);
            Date = SelectedTransaction.Date;
            Description = SelectedTransaction.Description;

            Validate();
        }

        public void UpdateTransaction()
        {
            if (HasErrors) // This shouldn't happen but if it would, the app will not crash
            {
                windowManager.ShowMessageBox("Fields were not filled correctly, check for errors.", "Error",
                    icon: MessageBoxImage.Error);
                return;
            }

            try
            {
                SelectedTransaction.Type = SelectedType;
                SelectedTransaction.CoinPrice = Convert.ToDecimal(CoinPrice, CultureInfo.InvariantCulture);
                SelectedTransaction.Amount = Convert.ToDecimal(Amount, CultureInfo.InvariantCulture);
                SelectedTransaction.Fee = Convert.ToDecimal(Fee, CultureInfo.InvariantCulture);
                SelectedTransaction.Date = Date;
                SelectedTransaction.Description = Description;

                eventAggregator.Publish(new TransactionsUpdatedEvent {Transaction = SelectedTransaction});
            }
            catch (Exception e)
            {
                windowManager.ShowMessageBox("Something bad happened during transaction creation. Try again.", "Error",
                    icon: MessageBoxImage.Error);
            }
        }

        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);
            NotifyOfPropertyChange(() => CanSubmit);
        }
    }
}