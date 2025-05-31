using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Goals;

public record GoalUpdate(
    Guid? Id,
    [StringLength(100)] string? Name,
    [Range(0.01, double.MaxValue)] decimal? Target,
    [Range(0.0, double.MaxValue)] decimal? Balance,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    GoalType? GoalType,
    DateTime? Due
);