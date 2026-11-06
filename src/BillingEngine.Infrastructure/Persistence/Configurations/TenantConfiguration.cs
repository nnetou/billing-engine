using BillingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingEngine.Infrastructure.Persistence.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Name)
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();
        }
    }
}