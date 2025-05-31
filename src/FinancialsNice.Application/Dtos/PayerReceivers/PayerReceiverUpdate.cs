using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.PayerReceivers
{
    public record PayerReceiverUpdate(
        [StringLength(100)] string? Name,
        [StringLength(255)] string? Description,
        [Url] string? ImgUrl,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] UserType? UserType
    );
}