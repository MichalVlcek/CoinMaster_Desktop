using System.Collections.Generic;
using CoinMaster.Model;
using Stylet;

namespace CoinMaster.ViewModel
{
    public class HomeScreenViewModel : Screen
    {
        public List<Coin> Coins => TmpDatabase.Coins;
    }
}