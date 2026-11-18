using BillingEngine.Application.Authentication;
using BCrypt.Net;

namespace BillingEngine.Infrastructure.Authentication;

public class BcryptPasswordHasher : IPasswordHasher
{
    // O custo 13 atrasa a geração propositadamente para mitigar ataques de força bruta
    private const int WorkFactor = 13;

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}