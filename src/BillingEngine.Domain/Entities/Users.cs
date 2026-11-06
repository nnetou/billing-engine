using BillingEngine.Domain.Enums;

namespace BillingEngine.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public Guid TenantId { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public Tenant? Tenant { get; private set; }

        protected User() { }

        public User(Guid tenantId, string email, string passwordHash, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password cannot be empty.", nameof(passwordHash));

            Id = Guid.NewGuid();
            TenantId = tenantId;
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
            CreatedAtUtc = DateTime.UtcNow;
        }

    }
}