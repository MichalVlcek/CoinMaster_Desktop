using Stylet;

namespace CoinMaster.ViewModel
{
    public class MenuBarViewModel : Screen
    {
        private RootViewModel GetParent => Parent as RootViewModel;
        
        public void OpenAddCoins()
        {
            GetParent?.ActivateItem(GetParent.AddCoinScreen);
        }

        public void OpenHomeScreen()
        {
            GetParent?.ActivateItem(GetParent.HomeScreen);
        }
    }
}