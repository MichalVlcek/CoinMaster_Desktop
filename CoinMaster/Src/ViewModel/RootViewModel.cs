using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public sealed class RootViewModel : Conductor<IScreen>.StackNavigation, INavigationControllerDelegate
    {
        public MenuBarViewModel MenuBar { get; }

        public RootViewModel(MenuBarViewModel menuBar)
        {
            MenuBar = menuBar;
        }

        public void NavigateTo(IScreen screen)
        {
            ActivateItem(screen);
        }
    }
}