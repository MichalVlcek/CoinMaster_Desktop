using CoinMaster.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoinMaster.DB
{
    public class CoinDataContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }

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
        }
    }
}