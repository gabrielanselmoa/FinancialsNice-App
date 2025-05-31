using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public PaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public int Installments { get; set; }
    public decimal ValuePerInstallment { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public Guid? CardId { get; set; }
    public Card? Card { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
}