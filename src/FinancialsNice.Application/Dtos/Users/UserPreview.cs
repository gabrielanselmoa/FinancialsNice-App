using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Users;

public record UserPreview
{
    public string Name { get; init; }
    public string Email { get; init; } 
    public string? ImgUrl { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserType Type { get; init; }
};