using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Users;

public record UserUpdate(
    [StringLength(100)] string? Name,
    [EmailAddress] string? Email,
    DateOnly? BirthDate,
    [Phone, StringLength(20)] string? Phone,
    string? ImgUrl,
    AddressUpdate? Address,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] UserType? Type
);