using FinancialsNice.Application.Dtos.Auth;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ISecurityService securityService, IAuthService authService,
        ILogger<AuthController> logger)
    {
        _securityService = securityService;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            var response = await _authService.SignUp(request);
            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during user sign-up.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var response = await _authService.SignIn(request);
            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during user sign-in.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost("validate-token")]
    public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenRequest request)
    {
        try
        {
            var isValid = await _securityService.ValidateToken(request);
            return StatusCode(200, isValid);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error validating token.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] EmailRequest request)
    {
        try
        {
            await _authService.ForgotPassword(request);
            return StatusCode(200, "Password reset accepted");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during forgot password request for email: {Email}", request.Email);
            return StatusCode(400, e.Message);
        }
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] SendPassword sendPassword)
    {
        try
        {
            await _authService.ResetPassword(sendPassword);
            return StatusCode(200, "Password updated!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during password reset.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet("email-confirmation")]
    public async Task<IActionResult> ConfirmEmail()
    {
        try
        {
            var userId = UserClaim.GetUserIdByClaims(User);
            if (userId == null)
            {
                _logger.LogWarning("Attempted email confirmation without authenticated user ID in claims.");
                return StatusCode(401, "Unauthorized: Owner ID not found in claims.");
            }

            var response = await _authService.ConfirmEmail(userId.Value);
            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during email confirmation for user ID: {OwnerId}",
                UserClaim.GetUserIdByClaims(User));
            return StatusCode(400, e.Message);
        }
    }
}