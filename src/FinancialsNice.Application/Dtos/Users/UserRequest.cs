using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Users;

public record UserRequest(
    string Name,
    DateOnly BirthDate,
    string Phone,
    string? ImgUrl,
    AddressRequest? Address,
    ICollection<CardRequest?> Cards,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    UserType Type,
    ICollection<RoleRequest?> Roles
);