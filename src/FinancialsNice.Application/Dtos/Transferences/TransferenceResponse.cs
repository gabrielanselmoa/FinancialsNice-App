namespace FinancialsNice.Application.Dtos.Transferences;

public record TransferenceResponse
{
    public Guid Id { get; init; }
    public string Currency { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public DateOnly SentAt { get; init; }
};