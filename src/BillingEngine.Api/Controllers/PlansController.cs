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
public class PlansController : ControllerBase
{
    private readonly BillingDbContext _context;

    public PlansController(BillingDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        List<Plan> plans = await _context.Plans.ToListAsync();

        return Ok(plans);
    }

    [HttpPost("{id}/updatePlan")]
    public async Task<IActionResult> UpdatePlanValues(Guid id, [FromBody] UpdatePlanDto request)
    {
        var plan = await _context.Plans.FirstOrDefaultAsync(p => p.Id == id);
        if (plan == null)
        {
            return NotFound(new { message = "O Plano não foi encontrado." });
        }

        plan.UpdateValues(request.NewIncludedRequests, request.NewPricePerUser);
        await _context.SaveChangesAsync();

        return Ok(plan);
    }
}

public record UpdatePlanDto(int NewIncludedRequests, decimal NewPricePerUser);