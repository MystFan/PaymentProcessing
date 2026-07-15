using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentProcessing.Domain;

namespace PaymentProcessing.DataAccess
{
    public class EFContext(DbContextOptions<EFContext> options, IOptions<DatabaseOptions>? databaseOptions) : DbContext(options)
    {
        private readonly IOptions<DatabaseOptions>? databaseOptions = databaseOptions;

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(databaseOptions!.Value.ConnectionString);
        }
    }
}
