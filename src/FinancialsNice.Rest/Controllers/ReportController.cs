using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("report")]
public class ReportController(IReportService reportService, ILogger<IReportService> logger)
    : ControllerBase
{
    [HttpGet]
    [Route("generate")]
    public async Task<IActionResult> Generate()
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var bytes = await reportService.GenerateReport(userId.Value)!;
            
            if (!bytes.Any())
            {
                logger.LogError("Report generation resulted in empty or null bytes for user ID: {OwnerId}.",
                    userId.Value);
                return StatusCode(500, "An internal error occurred while generating the report.");
            }

            var fileName = "FinancialReport.pdf";
            return File(bytes, "application/pdf", fileName);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error generating financial report for user ID: {OwnerId}.", userId);
            return StatusCode(500, "An internal error occurred while generating the report.");
        }
    }
}