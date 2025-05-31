using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Payments
{
    public record PaymentUpdate(
        [property: JsonConverter(typeof(JsonStringEnumConverter))] PaymentType? PaymentType,
        Guid? Id,
        [Range(0.01, double.MaxValue)] Decimal? Amount,
        [Range(0,36)] int? Installments,
        Guid? CardId
    );
}