using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<CurrencyModel> Currencies { get; set; }
        public DbSet<OperationModel> Operations { get; set; }
        public DbSet<WalletModel> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
