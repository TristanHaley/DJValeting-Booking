using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.CustomerId);

            builder.Property(e => e.CustomerId)
                   .HasColumnName("CustomerID")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Firstname)
                   .IsRequired();

            builder.Property(e => e.Surname)
                   .IsRequired();

            builder.Property(e => e.ContactNumber)
                   .IsRequired();

            builder.Property(e => e.ContactEmail)
                   .IsRequired();

            // Has many bookings, with one customer
            builder.HasMany(d => d.Bookings)
                   .WithOne(p => p.Customer)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}