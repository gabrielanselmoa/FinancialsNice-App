using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("wallets")]
public class WalletController(IWalletService walletService, ILogger<WalletController> logger)
    : ControllerBase
{
    private readonly IWalletService _walletService = walletService;
    private readonly ILogger<WalletController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetWallet()
    {
        var userId = UserClaim.GetUserIdByClaims(User);
        if (userId == null) return StatusCode(401, "Unauthorized");

        try
        {
            var wallet = await _walletService.GetAsync(userId.Value);
            return StatusCode(200, wallet);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error fetching user's' wallet.");
            return StatusCode(400, e.Message);
        }
    }
}