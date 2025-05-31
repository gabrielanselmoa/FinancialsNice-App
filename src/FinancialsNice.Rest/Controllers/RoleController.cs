using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("roles")]
public class RoleController(IRoleService roleService, ILogger<RoleController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await roleService.GetAllAsync();
            return StatusCode(200, roles);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all roles.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(
        [FromRoute] int id)
    {
        try
        {
            var role = await roleService.GetByIdAsync(id);
            if (role.Data == null)
            {
                return StatusCode(404, $"Role with ID {id} not found.");
            }

            return StatusCode(200, role);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching role by ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
    {
        try
        {
            var role = await roleService.CreateAsync(request);
            return StatusCode(201, role);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating role.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(
        [FromRoute] int id,
        [FromBody] RoleUpdate update)
    {
        try
        {
            var role = await roleService.UpdateAsync(id, update);
            if (role.Data == null)
            {
                return StatusCode(404, $"Role with ID {id} not found.");
            }

            return StatusCode(200, role);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating role with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteRole(
        [FromRoute] int id)
    {
        try
        {
            var result = await roleService.SoftDeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(404, $"Role with ID {id} not found or could not be soft-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error soft-deleting role with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteRole(
        [FromRoute] int id)
    {
        try
        {
            var result = await roleService.HardDeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(404, $"Role with ID {id} not found or could not be hard-deleted.");
            }

            return StatusCode(204);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error hard-deleting role with ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}