using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoinMaster.DB
{
    public partial class CoinDataContext : DbContext
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Database=CoinDB";
            
        public DbSet<Coin> Coins { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConnectionString);
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coin>()
                .HasKey(c => c.Id);
        }
    }
}
