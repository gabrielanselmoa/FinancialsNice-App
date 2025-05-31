using FinancialsNice.Application.Dtos.Permissions;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("permissions")]
public class PermissionController(IPermissionService permissionService, ILogger<PermissionController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        try
        {
            var permissions = await permissionService.GetAllAsync();
            return StatusCode(200, permissions);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all permissions.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermissionById(
        [FromRoute] int id)
    {
        try
        {
            var permission = await permissionService.GetByIdAsync(id);
            if (permission.Data == null)
            {
                return StatusCode(404, $"Permission with ID {id} not found.");
            }

            return StatusCode(200, permission);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching permission by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] PermissionRequest request)
    {
        try
        {
            var permission = await permissionService.CreateAsync(request);
            return StatusCode(201, permission);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating permission.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePermission(
        [FromRoute] int id,
        [FromBody] PermissionUpdate update)
    {
        try
        {
            var permission = await permissionService.UpdateAsync(id, update);
            if (permission.Data == null)
            {
                return StatusCode(404, $"Permission with ID {id} not found.");
            }

            return StatusCode(200, permission);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating permission with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeletePermission(
        [FromRoute] int id)
    {
        try
        {
            var result = await permissionService.SoftDeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(404, $"Permission with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting permission with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeletePermission(
        [FromRoute] int id)
    {
        try
        {
            var result = await permissionService.HardDeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(404, $"Permission with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting permission with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}