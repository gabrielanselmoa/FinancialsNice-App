using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class PayerReceiver
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public Status Status { get; set; }
    public UserType Type { get; set; }
}