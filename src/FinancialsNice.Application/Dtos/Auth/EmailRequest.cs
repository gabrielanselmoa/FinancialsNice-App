using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Auth;

public record EmailRequest
(
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    string Email
);