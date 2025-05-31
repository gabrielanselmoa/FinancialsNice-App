using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("payments")]
public class PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    : ControllerBase 
{
    [HttpGet]
    public async Task<IActionResult> GetAllPayments(
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var payments = await paymentService.GetAllAsync(page, perPage, userId.Value);
            return StatusCode(200, payments);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all payments.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var payment = await paymentService.GetByIdAsync(id, userId.Value);
            if (payment.Data == null)
            {
                return StatusCode(404, $"Payment with ID {id} not found.");
            }

            return StatusCode(200, payment);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching payment by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(
        [FromRoute] Guid id,
        [FromBody] PaymentUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var payment = await paymentService.UpdateAsync(id, userId.Value, update);
            if (payment.Data == null)
            {
                logger.LogWarning("Payment with ID {Id} not found for update.", id);
                return StatusCode(404, $"Payment with ID {id} not found.");
            }

            return StatusCode(200, payment);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating payment with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeletePayment(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await paymentService.SoftDeleteAsync(id, userId.Value);
            // Assuming SoftDeleteAsync returns a ResultModel<bool> where .Success indicates success
            if (!result.Success)
            {
                logger.LogWarning("Payment with ID {Id} not found for soft delete.", id);
                return StatusCode(404, $"Payment with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting payment with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeletePayment(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await paymentService.HardDeleteAsync(id, userId.Value);
            if (!result.Success)
            {
                logger.LogWarning("Payment with ID {Id} not found for hard delete.", id);
                return StatusCode(404, $"Payment with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting payment with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}