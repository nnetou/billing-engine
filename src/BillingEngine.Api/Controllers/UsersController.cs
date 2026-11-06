using BillingEngine.Application.Authentication;
using BillingEngine.Application.DTOs;
using BillingEngine.Domain.Entities;
using BillingEngine.Domain.Enums;
using BillingEngine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly BillingDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UsersController(
        BillingDbContext context,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email.ToLower());
        if (emailExists)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Conflito de Email",
                detail: "Este email já se encontra registado."
            );
        }

        var tenant = new Tenant(request.TenantName);
        _context.Tenants.Add(tenant);

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = new User(
            tenantId: tenant.Id,
            email: request.Email,
            passwordHash: hashedPassword,
            role: UserRole.Admin
        );
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLower());

        if (user is null)
        {
            return Unauthorized(new { message = "Credenciais inválidas." });
        }

        var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Credenciais inválidas." });
        }

        var token = _jwtProvider.Generate(user);

        return Ok(new TokenResponse(token));
    }
}