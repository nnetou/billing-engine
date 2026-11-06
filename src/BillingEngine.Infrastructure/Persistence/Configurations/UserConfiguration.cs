using BillingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingEngine.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("users");

		builder.HasKey(u => u.Id);
		builder.Property(u => u.Id)
			.HasColumnName("id");

        builder.Property(u => u.TenantId)
            .HasColumnName("tenant_id")
            .IsRequired();
        
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(u => u.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();
        
        // email must be unique within the same tenant
        builder.HasIndex(u => new { u.TenantId, u.Email })
            .IsUnique();

        // Relationship between User and Tenant
        builder.HasOne(u => u.Tenant)
            .WithMany() 
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
	}
}
