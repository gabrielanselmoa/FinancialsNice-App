using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Tickets;

public record TicketResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Subject { get; init; }
    public string? Message { get; init; }
    public UserPreview Client { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsResolved { get; init; }
    public DateTime? ResolvedAt { get; init; }
    public UserPreview? ResolvedBy { get; init; }
    [property: JsonConverter(typeof(JsonStringEnumConverter))] 
    public TicketType Type { get; init; }
}
