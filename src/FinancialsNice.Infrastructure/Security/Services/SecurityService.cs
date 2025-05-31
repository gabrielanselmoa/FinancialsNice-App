using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialsNice.Application.Dtos.Auth;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FinancialsNice.Infrastructure.Security.Services;

public class SecurityService : ISecurityService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly PasswordHasher<User> _passwordHasher;

    // public SecurityService(IConfiguration config)
    // {
    //     _secretKey = config["Jwt:Secret"]!;
    //     _issuer = config["Jwt:Issuer"]!;
    //     _audience = config["Jwt:Audience"]!;
    //     _passwordHasher = new PasswordHasher<Owner>();
    // }

    public SecurityService()
    {
        _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        _issuer = Environment.GetEnvironmentVariable("ISSUER")!;
        _audience = Environment.GetEnvironmentVariable("AUDIENCE")!;
        _passwordHasher = new PasswordHasher<User>();
    }

    public string GenerateJwtToken(User user, int duration, string timeUnit)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var unitType = DateTime.UtcNow.AddDays(1);
        if (timeUnit.Equals("min")) unitType = DateTime.UtcNow.AddMinutes(duration);

        var roles = user.Roles.Select(r => r.Name);
        var permissions = user.Roles
            .SelectMany(r => r.Permissions!)
            .Select(p => p.SlugName);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _issuer,
            Audience = _audience,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = unitType,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var role in roles) tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
        foreach (var permission in permissions) tokenDescriptor.Subject.AddClaim(new Claim("permission", permission));

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Guid? GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        return null;
    }

    public async Task<bool> ValidateToken(ValidateTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // Valida o token de forma assíncrona
            await Task.Run(() =>
                tokenHandler.ValidateToken(request.Token, tokenValidationParameters, out var validatedToken));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string providedPassword, string storedHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, storedHash, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}