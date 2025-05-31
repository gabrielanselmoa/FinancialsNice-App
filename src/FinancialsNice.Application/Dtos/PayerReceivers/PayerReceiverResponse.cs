using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.PayerReceivers
{
    public record PayerReceiverResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string? ImgUrl { get; init; }
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; init; }
    }
}