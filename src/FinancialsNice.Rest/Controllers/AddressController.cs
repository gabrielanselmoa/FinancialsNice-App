using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("addresses")]
public class AddressController(IAddressService addressService, ILogger<AddressController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAddresses([FromQuery] int page = 1, [FromQuery] int perPage = 20)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var addresses = await addressService.GetAllAsync(page, perPage, userId.Value);
            return StatusCode(200, addresses);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all addresses.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var address = await addressService.GetByIdAsync(id, userId.Value);
            return StatusCode(200, address);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching address by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] AddressRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var address = await addressService.CreateAsync(request, userId.Value);
            return StatusCode(201, address);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating address.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress([FromRoute] Guid id, [FromBody] AddressUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var address = await addressService.UpdateAsync(id, userId.Value, update);
            return StatusCode(200, address);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating address with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteAddress([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await addressService.SoftDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting address with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteAddress([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await addressService.HardDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting address with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}