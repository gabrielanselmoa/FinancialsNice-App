using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Card
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string Flag { get; set; } = null!;
    public string ExpiredAt { get; set; } = null!;
    public CardType CardType { get; set; }
    public Status Status { get; set; }
    public string Colors { get; set; } =  null!;
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    [JsonIgnore] public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
}