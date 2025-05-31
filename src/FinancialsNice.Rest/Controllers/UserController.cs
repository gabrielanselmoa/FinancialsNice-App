using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("users")]
public class UserController(IUserService service, ILogger<UserController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20)
    {
        try
        {
            var response = await service.GetAllAsync(page, perPage);
            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all users.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id)
    {
        try
        {
            var response = await service.GetByIdAsync(id);
            if (response.Data == null) return StatusCode(404, $"Owner with ID {id} not found.");

            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching user by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("logged/id")]
    public async Task<IActionResult> GetLoggedUser()
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var response = await service.GetByIdAsync(userId.Value);
            if (response.Data == null)
            {
                logger.LogWarning("Logged-in user with ID {OwnerId} not found in the system.", userId.Value);
                return StatusCode(404, $"Logged-in user with ID {userId.Value} not found.");
            }

            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching logged-in user details.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetUserByEmail(
        [FromQuery] string email)
    {
        try
        {
            var response = await service.GetByEmailAsync(email);
            if (response.Data == null)
            {
                return StatusCode(404, $"Owner with email {email} not found.");
            }

            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching user by email: {Email}", email);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null)
        {
            logger.LogWarning("Attempted to update user without authentication.");
            return StatusCode(401, "Unauthorized");
        }

        try
        {
            var response = await service.UpdateAsync(userId.Value, update);
            if (response.Data == null)
            {
                logger.LogWarning("{Message}", response.Message);
                return StatusCode(404, response);
            }

            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating user with ID: {Id}", userId);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteUser(
        [FromRoute] Guid id)
    {
        try
        {
            var response = await service.SoftDeleteAsync(id);
            if (!response.Success)
            {
                return StatusCode(404, $"Owner with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting user with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteUser(
        [FromRoute] Guid id)
    {
        try
        {
            var result = await service.HardDeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(404, $"Owner with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting user with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}