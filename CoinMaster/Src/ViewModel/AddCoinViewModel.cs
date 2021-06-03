using System.Collections.Generic;
using CoinMaster.Src.Core;
using CoinMaster.Src.Model;

namespace CoinMaster.ViewModel
{
    public class AddCoinViewModel : ObservableObject
    {
        public List<Coin> Coins { get; set; }
    }
}