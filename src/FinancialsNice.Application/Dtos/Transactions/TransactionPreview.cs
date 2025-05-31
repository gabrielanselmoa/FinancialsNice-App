using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Transactions;

public record TransactionPreview
{
    public Guid Id { get; init; }
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; init; }
    public string? Description { get; init; }
    public DateOnly Date { get; init; }
}