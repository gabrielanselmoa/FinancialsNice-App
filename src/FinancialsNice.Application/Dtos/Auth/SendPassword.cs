using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Auth;

public record SendPassword(
    [Required(ErrorMessage = "A senha é obrigatória.")] string Password,
    [Required(ErrorMessage = "O token é obrigatório.")] string Token
    );