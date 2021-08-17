using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(e => e.AdministratorId);

            builder.Property(e => e.AdministratorId)
                   .HasColumnName("AdministratorID")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Email)
                   .IsRequired();

            builder.Property(e => e.Password)
                   .IsRequired();
        }
    }
}