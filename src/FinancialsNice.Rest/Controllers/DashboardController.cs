using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("dashboard")]
public class DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTotalAmountByMonth(
        [FromQuery] int? year,
        [FromQuery] string[]? statuses)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await dashboardService.GetTotalAmountByMonth(userId.Value, year, statuses);
            return StatusCode(200, result);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Error retrieving dashboard data for user ID: {OwnerId}, year: {Year} and statuses typed!",
                userId, year);
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("total-earned")]
    public async Task<IActionResult> TotalEarned([FromQuery] int? year, [FromQuery] string[]? statuses)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await dashboardService.TotalEarned(userId.Value, year, statuses);
            return StatusCode(200, result);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Error retrieving dashboard data for user ID: {OwnerId}, year: {Year} and statuses typed!",
                userId, year);
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("total-spent")]
    public async Task<IActionResult> TotalSpent([FromQuery] int? year, [FromQuery] string[]? statuses)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await dashboardService.TotalSpent(userId.Value, year, statuses);
            return StatusCode(200, result);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Error retrieving dashboard data for user ID: {OwnerId}, year: {Year} and statuses typed!",
                userId, year);
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("wallet-health")]
    public async Task<IActionResult> WalletHealth([FromQuery] int? year, [FromQuery] string[]? statuses)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await dashboardService.FinancialHealth(userId.Value, year, statuses);
            if (result.Data == null) return StatusCode(204, result.Message);
            return StatusCode(200, result);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Error retrieving dashboard data for user ID: {OwnerId}, year: {Year} and statuses typed!",
                userId, year);
            return StatusCode(400, e.Message);
        }
    }
}