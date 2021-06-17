using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMaster.DB;
using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;

namespace CoinMaster.Data
{
    public class TransactionRepository
    {
        private readonly CoinDataContext dataContext;

        public TransactionRepository(CoinDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task InsertTransaction(Transaction transaction)
        {
            await dataContext.AddAsync(transaction);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            dataContext.Update(transaction);
            await dataContext.SaveChangesAsync();
        }
        
        public async Task DeleteTransaction(Transaction transaction)
        {
            dataContext.Remove(transaction);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsForCoin(Coin coin)
        {
            try
            {
                return await dataContext.Transactions.Where(t => t.CoinId == coin.Id).ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Transaction>();
            }
        }

        // public async Task GetTransactionsAll(Transaction transaction)
        // {
        // } 
    }
}