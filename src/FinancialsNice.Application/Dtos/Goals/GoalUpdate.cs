using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Goals;

public record GoalUpdate(
    [StringLength(100)] string? Name,
    [Range(0.01, double.MaxValue)] decimal? Target,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    GoalType? GoalType,
    DateOnly? Due,
    ICollection<TransferenceRequest>? Transferences
);