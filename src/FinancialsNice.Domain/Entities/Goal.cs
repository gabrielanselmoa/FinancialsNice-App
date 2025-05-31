using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Goal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public decimal Target { get; set; }
    public decimal Balance { get; set; }
    public GoalType GoalType { get; set; }
    public DateTime Due { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Status Status { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
}