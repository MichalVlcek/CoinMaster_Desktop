using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMaster.Model;
using CoinMaster.Utility;
using Microsoft.EntityFrameworkCore;

namespace CoinMaster.DB
{
    public class TransactionRepository
    {
        private readonly Func<CoinDataContext> dataContext;
        private static User LoggedUser => Model.LoggedUser.User;

        public TransactionRepository(Func<CoinDataContext> dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task InsertTransaction(Transaction transaction)
        {
            await using var context = dataContext();

            context.Entry(transaction.Coin).State = EntityState.Unchanged;
            context.Entry(transaction.User).State = EntityState.Unchanged;
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            await using var context = dataContext();

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            await using var context = dataContext();

            context.Remove(transaction);
            await context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsForCoin(Coin coin)
        {
            await using var context = dataContext();

            try
            {
                return await context.Transactions
                    .Where(t => t.CoinId == coin.Id && t.UserId == LoggedUser.Id)
                    .OrderByDescending(t => t.Date)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Transaction>();
            }
        }

        public async Task<List<Transaction>> GetTransactionsAll()
        {
            await using var context = dataContext();

            var user = await GetUser(context);
            return user.Coins
                .Select(c => c.Transaction)
                .SelectMany(x => x)
                .ToList();
        }
        
        private async Task<User> GetUser(CoinDataContext context) =>
            await context.Users
                .Include(i => i.Coins)
                .ThenInclude(c => c.Transaction.Where(t => t.UserId == LoggedUser.Id))
                .FirstOrDefaultAsync(i => i.Id == LoggedUser.Id);
    }
}