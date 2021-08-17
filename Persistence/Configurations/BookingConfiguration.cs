using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(e => e.BookingId);

            builder.Property(e => e.BookingId)
                   .HasColumnName("BookingID")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.PreferredDate)
                   .IsRequired();

            builder.Property(e => e.DateLeeway)
                   .IsRequired();

            builder.Property(e => e.Notified)
                   .IsRequired();

            builder.Property(e => e.Status)
                   .IsRequired();

            // Has one customer with many bookings
            builder.HasOne(d => d.Customer)
                   .WithMany(p => p.Bookings)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            // Has one vehicle type with many bookings
            builder.HasOne(d => d.VehicleType)
                   .WithMany()
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}