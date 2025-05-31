using FinancialsNice.Application.Dtos.Auth;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Interfaces.Services;

public interface ISecurityService
{
    // Geração do token JWT
    string GenerateJwtToken(User user, int duration, string timeUnit);
    Guid? GetUserIdFromToken(string token);
    // Valida o token do Front-End
    Task<bool> ValidateToken(ValidateTokenRequest request);
    
    // Método para encriptar a senha (gerar hash)
    string HashPassword(User user, string password);
    
    // Método para verificar se a senha fornecida é válida
    bool VerifyPassword(User user, string providedPassword, string storedHash);
}