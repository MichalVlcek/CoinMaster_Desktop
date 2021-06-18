using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoinMaster.DB
{
    public class CoinDataContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coin>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Coin>()
                .Property(t => t.Price)
                .HasPrecision(20, 9);
            
            modelBuilder.Entity<Coin>()
                .Property(t => t.Ath)
                .HasPrecision(20, 9);
            
            modelBuilder.Entity<Coin>()
                .Property(t => t.Atl)
                .HasPrecision(20, 9);
            
            modelBuilder.Entity<Transaction>()
                .HasOne<Coin>(t => t.Coin)
                .WithMany(c => c.Transaction)
                .HasForeignKey(t => t.CoinId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CoinPrice)
                .HasPrecision(20, 9);
            
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(20, 9);
            
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Fee)
                .HasPrecision(20, 9);
        }
    }
}