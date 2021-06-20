using System.Collections.Generic;

namespace CoinMaster.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public ICollection<Coin> Coins { get; set; }
    }
}