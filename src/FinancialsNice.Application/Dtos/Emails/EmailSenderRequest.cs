using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Emails;

public record EmailSenderRequest(
    [Required(ErrorMessage = "O campo 'email' é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo 'email' deve conter um endereço de e-mail válido.")]
    string Email,

    [Required(ErrorMessage = "O campo 'subject' é obrigatório.")]
    [StringLength(100, ErrorMessage = "O assunto deve ter no máximo 100 caracteres.")]
    string Subject,

    [Required(ErrorMessage = "O campo 'body' é obrigatório.")]
    [MinLength(5, ErrorMessage = "O corpo da mensagem deve ter pelo menos 5 caracteres.")]
    string Body
);