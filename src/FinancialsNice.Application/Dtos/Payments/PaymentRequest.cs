using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Payments
{
    public record PaymentRequest(
        [property: JsonConverter(typeof(JsonStringEnumConverter))] PaymentType PaymentType,
        [Required, Range(0.01, double.MaxValue)] decimal Amount,
        [Range(0,24)] int Installments,
        Guid? CardId
    );
}