using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Transactions
{
    public record TransactionRequest(
        [StringLength(255)] string Description,
        [Required, EmailAddress, StringLength(150)] string Email,
        [Required, StringLength(3)] string Currency,
        [Required, Range(0.01, double.MaxValue)] decimal Amount,
        [Required] string ScheduledAt,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Category Category,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] TransactionType TransactionType,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] TransactionStatus Status,
        Guid? PayerReceiverId,
        [Required, MinLength(1)] ICollection<PaymentRequest> Payments
    );
}