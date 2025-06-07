using FinancialsNice.Application.Dtos.Dashboards;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<ResponseModel<ICollection<TotalAmountByMonth>>> GetTotalAmountByMonth(Guid id, int? year, string[]? statuses);
    Task<ResponseModel<TotalValue>> TotalEarned(Guid id, int? year, string[]? statuses);
    Task<ResponseModel<TotalValue>> TotalSpent(Guid id, int? year, string[]? statuses);
    Task<ResponseModel<HealthPercentage>> FinancialHealth(Guid id, int? year, string[]? statuses);
}