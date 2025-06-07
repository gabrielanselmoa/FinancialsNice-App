using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Goals;

public record GoalResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public decimal Target { get; init; }
    public decimal Balance { get; init; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GoalType GoalType { get; init; }
    public DateOnly Due { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ModifiedAt { get; init; }
    public ICollection<TransferenceResponse> Transferences { get; init; } = new List<TransferenceResponse>();
}
