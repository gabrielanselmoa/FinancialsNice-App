using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Transactions;

public record TransactionResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string? Description { get; init; }
    public string Email { get; init; } = null!;
    public string? Currency { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateOnly ScheduledAt { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionStatus Status { get; init; }
    public PayerReceiverResponse? PayerReceiver { get; init; }
    public ICollection<PaymentResponse> Payments { get; init; } = new List<PaymentResponse>();
}