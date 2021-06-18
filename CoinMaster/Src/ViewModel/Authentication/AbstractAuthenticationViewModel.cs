using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel.Authentication
{
    public class AbstractAuthenticationViewModel : Screen
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetAndNotify(ref _email, value);
                NotifyOfPropertyChange(() => CanAuthenticate);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetAndNotify(ref _password, value);
        }

        public bool CanAuthenticate => !HasErrors;

        public readonly INavigationControllerAuthentication NavigationController;
        public readonly IWindowManager WindowManager;

        public AbstractAuthenticationViewModel(
            IWindowManager windowManager,
            INavigationControllerAuthentication navigationController,
            IModelValidator<AbstractAuthenticationViewModel> validator) : base(validator)
        {
            this.WindowManager = windowManager;
            this.NavigationController = navigationController;
            Validate();
        }
    }
}