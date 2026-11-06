using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BillingEngine.Application.Authentication;
using BillingEngine.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BillingEngine.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var secretKey = _configuration["Jwt:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        // A "caneta" que carimba o token (usa a chave secreta e o algoritmo HmacSha256)
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // A informação escrita na pulseira VIP (Claims)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("tenant_id", user.TenantId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        // Montagem do Token final
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryInMinutes"]!)),
            signingCredentials: credentials);

        // Devolve o token em formato de string (texto)
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}