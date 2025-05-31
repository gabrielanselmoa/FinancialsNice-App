using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("cards")]
public class CardController(ICardService cardService, ILogger<CardController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var cards = await cardService.GetAllAsync(userId.Value);
            return StatusCode(200, cards);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all cards.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var card = await cardService.GetByIdAsync(id, userId.Value);
            if (card.Data == null)
            {
                // This indicates the card was not found for the given ID and user
                return StatusCode(404, $"Card with ID {id} not found.");
            }

            return StatusCode(200, card);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching card by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CardRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var card = await cardService.CreateAsync(request, userId.Value);
            return StatusCode(201, card);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating card.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] CardUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var card = await cardService.UpdateAsync(id, userId.Value, update);
            if (card.Data == null)
            {
                return StatusCode(404, $"Card with ID {id} not found.");
            }

            return StatusCode(200, card);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating card with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteCard([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await cardService.SoftDeleteAsync(id, userId.Value);
            if (!result.Success)
            {
                // Assuming result.Success being false indicates not found or other specific failure
                return StatusCode(404, $"Card with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204); // No Content on successful soft delete
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting card with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteCard([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await cardService.HardDeleteAsync(id, userId.Value);
            if (!result.Success)
            {
                return StatusCode(404, $"Card with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204); // No Content on successful hard delete, as per AddressController pattern
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting card with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}