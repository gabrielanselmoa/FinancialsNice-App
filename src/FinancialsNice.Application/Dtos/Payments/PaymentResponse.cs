using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Payments
{
    public record PaymentResponse
    {
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public int Installments { get; init; }
        public Decimal ValuePerInstallment { get; init; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType PaymentType { get; init; }
        public CardResponse? Card { get; init; }
    }
}