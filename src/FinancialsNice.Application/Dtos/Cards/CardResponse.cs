using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Cards
{
    public record CardResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } =  null!;
        public string Number { get; init; } = null!;
        public string Company { get; init; } = null!;
        public string Flag { get; init; } = null!;
        public string[]? Colors { get; init; }
        public string ExpiredAt { get; init; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CardType CardType { get; init; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; init; }

        // public ICollection<TransactionResponse>? Transactions { get; init; }
    }
}