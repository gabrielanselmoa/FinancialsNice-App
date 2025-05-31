using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Cards
{
    public record CardRequest(
        [Required, StringLength(100)] string Name,
        [Required] string Number,
        [Required, StringLength(50)] string Company,
        [Required, StringLength(50)] string Flag,
        [Required, StringLength(7)] string ExpiredAt,
        [Required] String[] Colors,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] CardType CardType
    );
}