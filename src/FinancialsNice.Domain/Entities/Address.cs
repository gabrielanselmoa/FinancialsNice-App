using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Address
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Neighborhood { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string? Complement { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid OwnerId { get; set; }
    [JsonIgnore] public User Owner { get; set; }
}