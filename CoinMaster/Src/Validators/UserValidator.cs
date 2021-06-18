using CoinMaster.ViewModel;
using FluentValidation;

namespace CoinMaster.Validators
{
    public class UserValidator : AbstractValidator<AbstractAuthenticationViewModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .EmailAddress().WithMessage("{PropertyName} must have email format.");
        }
    }
}