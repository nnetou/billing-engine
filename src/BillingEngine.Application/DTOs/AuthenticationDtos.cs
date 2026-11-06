namespace BillingEngine.Application.DTOs;

public record RegisterRequest(string TenantName, string Email, string Password);

public record LoginRequest(string Email, string Password);

public record TokenResponse(string Token);