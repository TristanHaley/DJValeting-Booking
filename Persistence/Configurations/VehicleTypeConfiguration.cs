using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasKey(e => e.VehicleTypeId);

            builder.Property(e => e.VehicleTypeId)
                   .HasColumnName("VehicleTypeID")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Price)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .IsRequired();
        }
    }
}