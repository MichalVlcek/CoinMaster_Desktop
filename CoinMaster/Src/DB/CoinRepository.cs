using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMaster.Api;
using CoinMaster.DB;
using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;

namespace CoinMaster.Data
{
    public class CoinRepository
    {
        private readonly Func<CoinDataContext> dataContext;

        public CoinRepository(Func<CoinDataContext> dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task InsertCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                await using var context = dataContext();
                if (context.Coins.Contains(coin))
                {
                    await UpdateCoin(coin);
                }
                else
                {
                    await context.AddAsync(coin);
                }

                await context.SaveChangesAsync();
            });
        }

        public async Task DeleteCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                await using var context = dataContext();

                context.Coins.Remove(coin);
                await context.SaveChangesAsync();
            });
        }

        public async Task<List<Coin>> GetAllFromDatabase()
        {
            await using var context = dataContext();

            await context.Transactions.ToListAsync(); // Loading transactions to bind them to Coin objects
            var coins =  await context.Coins
                .ToListAsync();

            return coins.OrderByDescending(c => c.HeldValue).ToList();
        }

        public async Task<List<Coin>> LoadAllCoins()
        {
            return await ApiService.LoadCoins();
        }

        public async Task<List<Coin>> LoadWatchedCoins()
        {
            await using var context = dataContext();

            var coins = await context.Coins.ToListAsync();
            if (!coins.Any()) return coins; // Needs to return when coins are empty, otherwise api would load all coins

            try
            {
                coins = await ApiService.LoadCoins(coins.ToArray());
                UpdateCoins(coins);
            }
            catch (Exception e)
            {
            }

            await context.Transactions.ToListAsync(); // Loading transactions to bind them to Coin objects
            coins = await context.Coins
                .ToListAsync();
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
    }
}