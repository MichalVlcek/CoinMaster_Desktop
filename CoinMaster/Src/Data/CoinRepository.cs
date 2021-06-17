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
        private readonly CoinDataContext dataContext;

        public CoinRepository(CoinDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task InsertCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                if (dataContext.Coins.Contains(coin))
                {
                    await UpdateCoin(coin);
                }
                else
                {
                    await dataContext.AddAsync(coin);
                }

                await dataContext.SaveChangesAsync();
            });
        }

        public async Task DeleteCoin(Coin coin)
        {
            await Task.Run(async () =>
            {
                dataContext.Coins.Remove(coin);
                await dataContext.SaveChangesAsync();
            });
        }

        public async Task<List<Coin>> GetAllFromDatabase()
        {
            return await dataContext.Coins.ToListAsync();
        }

        public async Task<List<Coin>> LoadAllCoins()
        {
            return await ApiService.LoadCoins();
        }

        public async Task<List<Coin>> LoadWatchedCoins()
        {
            var coins = await dataContext.Coins.ToListAsync();
            if (!coins.Any()) return coins; // Needs to return when coins are empty, otherwise api would load all coins

            try
            {
                coins = await ApiService.LoadCoins(coins.ToArray());
                UpdateCoins(coins);
            }
            catch (Exception e)
            {
            }

            return coins;
        }

        private async Task UpdateCoin(Coin coin)
        {
            var entity = await dataContext.Coins.FindAsync(coin.Id);
            if (entity != null)
            {
                dataContext.Entry(entity).State = EntityState.Detached;
            }

            dataContext.Coins.Update(coin);
        }

        private void UpdateCoins(List<Coin> coins) => coins.ForEach(async c => await UpdateCoin(c));
    }
}