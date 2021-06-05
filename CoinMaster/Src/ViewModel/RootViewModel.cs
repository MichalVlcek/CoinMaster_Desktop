using Stylet;

namespace CoinMaster.ViewModel
{
    public sealed class RootViewModel : Conductor<IScreen>.StackNavigation
    {
        public MenuBarViewModel MenuBar { get; private set; }
        public HomeScreenViewModel HomeScreen { get; set; }
        public AddCoinViewModel AddCoinScreen { get; set; }

        public RootViewModel(MenuBarViewModel menuBar, HomeScreenViewModel homeScreen, AddCoinViewModel addCoinScreen)
        {
            MenuBar = menuBar;
            ActivateItem(menuBar);
            
            HomeScreen = homeScreen;
            AddCoinScreen = addCoinScreen;
            
            ActivateItem(homeScreen);
        }
    }
}