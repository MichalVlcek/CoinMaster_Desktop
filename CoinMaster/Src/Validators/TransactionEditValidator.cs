﻿using CoinMaster.ViewModel;
using FluentValidation;

namespace CoinMaster.Validators
{
    public class TransactionEditValidator : AbstractValidator<TransactionEditViewModel>
    {
        private const string NumberRegex = "^[0-9][0-9.]*$";

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