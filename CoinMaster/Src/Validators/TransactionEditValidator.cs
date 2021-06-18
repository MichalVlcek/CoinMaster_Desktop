using CoinMaster.ViewModel;
using CoinMaster.ViewModel.CoinDetail;
using FluentValidation;

namespace CoinMaster.Validators
{
    public class TransactionEditValidator : AbstractValidator<TransactionEditViewModel>
    {
        private const string NumberRegex = @"^\d+\.?\d*$";

        public TransactionEditValidator()
        {
            RuleFor(x => x.CoinPrice)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .Matches(NumberRegex).WithMessage("{PropertyName} must be number");
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .Matches(NumberRegex).WithMessage("{PropertyName} must be number");
            RuleFor(x => x.Fee)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .Matches(NumberRegex).WithMessage("{PropertyName} must be number");
        }
    }
}