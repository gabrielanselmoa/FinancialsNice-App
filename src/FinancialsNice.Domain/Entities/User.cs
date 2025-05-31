using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } =  null!;
    public DateOnly? BirthDate { get; set; }
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public string? Phone { get; set; }
    public string? ImgUrl { get; set; }
    public bool Wizard { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Status Status { get; set; }
    public UserType Type { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } =  null!;
    public ICollection<Card>? Cards { get; set; } = new List<Card>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Goal>? Goals { get; set; } = new List<Goal>();
}