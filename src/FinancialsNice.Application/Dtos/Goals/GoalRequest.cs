using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Goals;

public record GoalRequest(
    [Required, StringLength(100)] string Name,
    [Required, Range(0.01, double.MaxValue)] decimal Target,
    [Required] [property: JsonConverter(typeof(JsonStringEnumConverter))] GoalType GoalType,
    [Required] string Due
);
