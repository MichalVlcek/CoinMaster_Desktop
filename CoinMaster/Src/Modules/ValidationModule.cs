using CoinMaster.Validators;
using FluentValidation;
using Stylet;
using StyletIoC;

namespace CoinMaster.Modules
{
    public class ValidationModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind(typeof(IModelValidator<>)).To(typeof(FluentModelValidator<>));
            Bind(typeof(IValidator<>)).ToAllImplementations();
        }
    }
}