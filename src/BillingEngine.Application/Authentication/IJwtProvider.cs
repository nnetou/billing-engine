using BillingEngine.Domain.Entities;

namespace BillingEngine.Application.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}