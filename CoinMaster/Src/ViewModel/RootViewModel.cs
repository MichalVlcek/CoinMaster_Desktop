using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public sealed class RootViewModel : Conductor<IScreen>.StackNavigation, INavigationControllerDelegate
    {
        public void NavigateTo(IScreen screen)
        {
            ActivateItem(screen);
        }
    }
}