using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!; 
    public string Subject { get; set; } = null!; 
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsResolved { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public Guid? ResolvedById { get; set; }
    public User? ResolvedBy { get; set; }
    public TicketType Type { get; set; }
    public Guid ClientId { get; set; }
    public User Client { get; set; } = null!;
}