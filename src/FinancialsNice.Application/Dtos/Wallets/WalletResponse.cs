namespace FinancialsNice.Application.Dtos.Wallets;

public record WalletResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public decimal Balance { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ModifiedAt { get; init; }
}
