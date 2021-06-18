using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class MainContentViewModel : Conductor<IScreen>.StackNavigation, INavigationControllerDelegate
    {
        public MenuBarViewModel MenuBar { get; }

        public MainContentViewModel(MenuBarViewModel menuBar)
        {
            MenuBar = menuBar;
        }
        
        public void NavigateTo(IScreen screen)
        {
            ActivateItem(screen);
        }
    }
}