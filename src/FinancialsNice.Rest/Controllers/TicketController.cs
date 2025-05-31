using FinancialsNice.Application.Dtos.Tickets;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _service;
    private readonly ILogger<TicketController> _logger;

    public TicketController(ITicketService service, ILogger<TicketController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTickets([FromQuery] int page = 1, [FromQuery] int perPage = 20)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var tickets = await _service.GetAllAsync(page, perPage, userId.Value);
            return StatusCode(200, tickets);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error fetching all tickets.");
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var ticket = await _service.GetByIdAsync(id, userId.Value);
            return StatusCode(200, ticket);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error fetching ticket by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] TicketRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var address = await _service.CreateAsync(request, userId.Value);
            return StatusCode(201, address);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a ticket.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket([FromRoute] Guid id, [FromBody] TicketUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var ticket = await _service.UpdateAsync(id, userId.Value, update);
            return StatusCode(200, ticket);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating ticket with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteTicket([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await _service.SoftDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error soft-deleting ticket with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteTicket([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await _service.HardDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error hard-deleting ticket with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}