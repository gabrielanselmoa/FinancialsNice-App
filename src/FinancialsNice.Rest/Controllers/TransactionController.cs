using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("transactions")]
public class TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions(
        [FromQuery] string? search,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var transactions =
                await transactionService.GetAllAsync(search, startDate, endDate, page, perPage, userId.Value);
            return StatusCode(200, transactions);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all transactions.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");
        
        try
        {
            var transaction = await transactionService.GetByIdAsync(id, userId.Value);
            if (transaction.Data == null)
            {
                return StatusCode(404, $"Transaction with ID {id} not found.");
            }

            return StatusCode(200, transaction);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching transaction by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("next-to-due")]
    public async Task<IActionResult> GetLastTransactions()
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var transaction = await transactionService.GetLastAsync(userId.Value);
            return StatusCode(200, transaction);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching transactions");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var transaction = await transactionService.CreateAsync(request, userId.Value);
            if (!transaction.Success) return StatusCode(400, transaction);
            return StatusCode(200, transaction);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating transaction.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(
        [FromRoute] Guid id,
        [FromBody] TransactionUpdate request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var transaction = await transactionService.UpdateAsync(id, userId.Value, request);
            if (transaction.Data == null)
            {
                return StatusCode(404, $"Transaction with ID {id} not found.");
            }

            return StatusCode(200, transaction);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating transaction with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteTransaction(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");
        
        try
        {
            var result = await transactionService.SoftDeleteAsync(id, userId.Value);
            if (!result.Success)
            {
                return StatusCode(404, $"Transaction with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting transaction with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteTransaction(
        [FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");
        
        try
        {
            var result = await transactionService.HardDeleteAsync(id, userId.Value);
            if (!result.Success)
            {
                return StatusCode(404, $"Transaction with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting transaction with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}