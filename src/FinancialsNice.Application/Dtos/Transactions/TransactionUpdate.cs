using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Transactions
{
    public record TransactionUpdate(
        [StringLength(255)] string? Description,
        [EmailAddress, StringLength(150)] string? Email,
        [StringLength(10)] string? Currency,
        [Range(0.01, double.MaxValue)] Decimal? Amount,
        string? ScheduledAt,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Category? Category,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] TransactionType? TransactionType,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] TransactionStatus? Status,
        Guid? PayerReceiverId,
        ICollection<PaymentUpdate>? Payments
    );
}