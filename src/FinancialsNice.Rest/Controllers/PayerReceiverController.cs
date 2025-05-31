using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("payer-receivers")]
public class PayerReceiverController(
    IPayerReceiverService payerReceiverService,
    ILogger<PayerReceiverController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPayerReceivers(
        [FromQuery] string? search,
        [FromQuery] UserType? type,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var people = await payerReceiverService.GetAllBySearchAsync(search, type, page, perPage, userId.Value);
            return StatusCode(200, people);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all payers/receivers by search.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayerReceiverById([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var person = await payerReceiverService.GetByIdAsync(id,  userId.Value);
            if (person.Data == null)
            {
                return StatusCode(404, $"Payer/Receiver with ID {id} not found.");
            }

            return StatusCode(200, person);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching payer/receiver by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayerReceiver([FromBody] PayerReceiverRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var person = await payerReceiverService.CreateAsync(request, userId.Value);
            return StatusCode(201, person); // Changed to 201 CreatedAt for new resource
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating payer/receiver.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayerReceiver(
        [FromRoute] Guid id,
        [FromBody] PayerReceiverUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var payment = await payerReceiverService.UpdateAsync(id,  userId.Value, update);
            if (payment.Data == null) 
            {
                return StatusCode(404, $"Payer/Receiver with ID {id} not found.");
            }

            return StatusCode(200, payment);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating payer/receiver with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }


    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeletePayerReceiver(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await payerReceiverService.SoftDeleteAsync(id,  userId.Value);
            if (!result.Success) 
            {
                return StatusCode(404, $"Payer/Receiver with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting payer/receiver with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeletePayerReceiver(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await payerReceiverService.HardDeleteAsync(id,  userId.Value);
            if (!result.Success) 
            {
                return StatusCode(404, $"Payer/Receiver with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204); 
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting payer/receiver with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}