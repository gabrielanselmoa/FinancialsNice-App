namespace FinancialsNice.Domain.Entities;

public class Transference
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string Currency { get; set; }
    public DateOnly SentAt { get; set; }
    public Guid GoalId { get; set; }
    public Goal Goal { get; set; }
}