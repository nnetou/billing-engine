using BillingEngine.Application.Authentication;
using BCrypt.Net;

namespace BillingEngine.Infrastructure.Authentication;

public class BcryptPasswordHasher : IPasswordHasher
{
    // O custo 13 atrasa a geração propositadamente para mitigar ataques de força bruta
    private const int WorkFactor = 13;

    public string Hash(string password)
    {
        // Usamos o EnhancedHashPassword porque o BCrypt clássico ignora 
        // silenciosamente qualquer caractere da password depois do 72º caractere.
        // O "Enhanced" resolve isso fazendo um hash SHA384 antes do BCrypt.
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}