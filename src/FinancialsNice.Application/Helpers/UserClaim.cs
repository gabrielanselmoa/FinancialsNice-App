using System.Security.Claims;

namespace FinancialsNice.Application.Helpers;

public static class UserClaim
{
    public static Guid? GetUserIdByClaims(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("Owner ID not found in claims.");
        }

        return Guid.Parse(userIdClaim.Value);
    }
}
