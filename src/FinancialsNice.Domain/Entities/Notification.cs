namespace FinancialsNice.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Message { get; set; } = null!;
    public string Type { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsRead { get; set; } = false;
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
}