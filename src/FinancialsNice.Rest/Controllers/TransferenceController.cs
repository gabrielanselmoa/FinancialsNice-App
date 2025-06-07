using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[ApiController]
[Route("transferences")]
public class TransferenceController (ITransferenceService service, ILogger<TransferenceController> logger) : ControllerBase
{
    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteTransference(
        [FromRoute] Guid id, [FromQuery] Guid goalId)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");
        
        try
        {
            var result = await service.HardDeleteAsync(id, userId.Value, goalId);
            if (!result.Success)
            {
                return StatusCode(404, $"Transference with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting transference with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}