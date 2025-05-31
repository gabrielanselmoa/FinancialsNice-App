using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateOnly ScheduledAt { get; set; }
    public Category Category { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public Status Status { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public Guid? PayerReceiverId { get; set; }
    public PayerReceiver? PayerReceiver { get; set; }
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } =  null!;
    public Guid? GoalId { get; set; }
    public Goal? Goal { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}