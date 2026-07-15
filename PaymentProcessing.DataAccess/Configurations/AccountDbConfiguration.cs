using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessing.Domain;

namespace PaymentProcessing.DataAccess.Configurations
{
    internal class AccountDbConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));

            builder.Property(e => e.Currency)
                .HasMaxLength(Constants.Account.CurrencyMaxLength);
        }
    }
}
