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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplica automaticamente todas as configurações que herdam de IEntityTypeConfiguration nesta assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);
    }
}