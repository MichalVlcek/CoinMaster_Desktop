using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMaster.Api;
using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;

namespace CoinMaster.DB
{
    public class CoinRepository
    {
        private readonly Func<CoinDataContext> dataContext;
        private static User LoggedUser => Model.LoggedUser.User;

        public CoinRepository(Func<CoinDataContext> dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task InsertCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                await using var context = dataContext();
                var user = await GetUser(context);
                
                if (user.Coins.Contains(coin))
                {
                    await UpdateCoin(coin);
                }
                else
                {
                    var insertCoin = coin;
                    if (context.Coins.Contains(coin))
                    {
                        insertCoin = await context.Coins.FindAsync(coin.Id);
                    }

                    user.Coins.Add(insertCoin);
                }

                await context.SaveChangesAsync();
            });
        }

        public async Task DeleteCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                await using var context = dataContext();
                var user = await GetUser(context);

                user.Coins.Remove(await context.Coins.FindAsync(coin.Id));
                await context.SaveChangesAsync();
            });
        }

        public async Task<List<Coin>> GetAllFromDatabase()
        {
            await using var context = dataContext();
            var user = await GetUser(context);

            var coins = user.Coins;

            return coins.OrderByDescending(c => c.HeldValue).ToList();
        }

        public async Task<List<Coin>> LoadAllCoins()
        {
            return await ApiService.LoadCoins();
        }

        public async Task<List<Coin>> LoadWatchedCoins()
        {
            await using var context = dataContext();
            var user = await GetUser(context);

            var coins = user.Coins.ToList();
            if (!coins.Any()) return coins; // Needs to return when coins are empty, otherwise api would load all coins

            try
            {
                coins = await ApiService.LoadCoins(coins.ToArray());
                UpdateCoins(coins);
            }
            catch (Exception e)
            {
            }

            user = await GetUser(context); // need to get user again to include transactions to coin
            coins = user.Coins.ToList();
            return coins.OrderByDescending(c => c.HeldValue).ToList();
        }

        private async Task UpdateCoin(Coin coin)
        {
            await using var context = dataContext();

            var entity = await context.Coins.FindAsync(coin.Id);
            if (entity != null)
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            context.Coins.Update(coin);
        }

        private void UpdateCoins(List<Coin> coins) => coins.ForEach(async c => await UpdateCoin(c));

        private async Task<User> GetUser(CoinDataContext context) =>
            await context.Users
                .Include(i => i.Coins)
                .ThenInclude(c => c.Transaction.Where(t => t.UserId == LoggedUser.Id))
                .FirstOrDefaultAsync(i => i.Id == LoggedUser.Id);
    }
}