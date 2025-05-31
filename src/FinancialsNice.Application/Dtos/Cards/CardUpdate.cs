using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Cards;

public record CardUpdate
{
    public string? Name { get; init; }
    public string? Number { get; init; }
    public string? Company { get; init; }
    public string? Flag { get; init; }
    public string[]? Colors { get; init; }
    public string? ExpiredAt { get; init; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CardType? CardType { get; init; }
}