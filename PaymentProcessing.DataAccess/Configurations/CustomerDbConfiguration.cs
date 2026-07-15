using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessing.Domain;

namespace PaymentProcessing.DataAccess.Configurations
{
    internal class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

            builder.Property(e => e.Name)
                .HasMaxLength(Constants.Customer.NameMaxLength);

            builder.Property(e => e.Email)
                .HasMaxLength(Constants.Customer.EmailMaxLength);
        }
    }
}
