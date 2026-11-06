namespace BillingEngine.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        protected Tenant() { }

        public Tenant(String name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim();
            IsActive = true;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;

    }
}