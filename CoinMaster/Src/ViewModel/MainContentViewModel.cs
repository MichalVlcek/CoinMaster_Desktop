using CoinMaster.Interfaces;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class MainContentViewModel : Conductor<IScreen>.StackNavigation, INavigationControllerDelegate
    {
        public MenuBarViewModel MenuBar { get; }

        private readonly INavigationControllerMain navigationControllerMain;

        public MainContentViewModel(INavigationControllerMain navigationControllerMain, MenuBarViewModel menuBar)
        {
            this.navigationControllerMain = navigationControllerMain;
            MenuBar = menuBar;
        }
        
        public void NavigateTo(IScreen screen)
        {
            ActivateItem(screen);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            navigationControllerMain.NavigateToHomeScreen();
        }
    }
}