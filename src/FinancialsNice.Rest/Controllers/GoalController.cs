using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Rest.Swagger.Examples.Goal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("goals")]
public class GoalController(IGoalService goalService, ILogger<GoalController> logger)
    : ControllerBase
{
    private readonly IGoalService _goalService = goalService;
    private readonly ILogger<GoalController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAllGoals([FromQuery] string? search,
        [FromQuery] int page = 1, [FromQuery] int perPage = 2)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "You do not have authorization!");

        try
        {
            var goals = await _goalService.GetAllAsync(page, perPage, userId.Value, search);
            return StatusCode(200, goals);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao buscar todas as metas.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGoalById([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var goal = await _goalService.GetByIdAsync(id, userId.Value);
            return StatusCode(200, goal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao buscar meta por ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] GoalRequest request)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var goal = await _goalService.CreateAsync(request, userId.Value);
            return StatusCode(201, goal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "API: Creating a new goal has failed!");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Goal200ResponseExample), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(Goal200ResponseExample))]
    public async Task<IActionResult> UpdateGoal([FromRoute] Guid id, [FromBody] GoalUpdate update)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "juju");

        try
        {
            var goal = await _goalService.UpdateAsync(id, userId.Value, update);
            return StatusCode(200, goal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao atualizar meta com ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteGoal([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "You're not allowed");

        try
        {
            var result = await _goalService.SoftDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao excluir meta com ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<IActionResult> HardDeleteGoal([FromRoute] Guid id)
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var result = await _goalService.HardDeleteAsync(id, userId.Value);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao excluir meta com ID: {Id}", id);
            return StatusCode(400, e.Message);
        }
    }
}