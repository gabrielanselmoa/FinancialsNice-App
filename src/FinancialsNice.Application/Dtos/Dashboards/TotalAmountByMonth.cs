namespace FinancialsNice.Application.Dtos.Dashboards;

public record TotalAmountByMonth
{
    public string? Month { get; init; }
    public decimal Paid { get; init; }
    public decimal Received { get; init; }
};