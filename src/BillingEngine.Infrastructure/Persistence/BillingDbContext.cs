using BillingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillingEngine.Infrastructure.Persistence;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Plan> Plans => Set<Plan>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Plan>().HasData(
            new Plan(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Free", 50, 0m),
            new Plan(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Pro", 5000, 20m),
            new Plan(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Enterprise", 10000, 50m)
        );
    }
}