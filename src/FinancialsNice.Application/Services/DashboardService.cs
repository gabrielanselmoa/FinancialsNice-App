using System.Globalization;
using FinancialsNice.Application.Dtos.Dashboards;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class DashboardService(ITransactionRepository transactionRepository, IUserRepository userRepository)
    : IDashboardService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResponseModel<ICollection<TotalAmountByMonth>>> GetTotalAmountByMonth(Guid userId, int? year,
        string[]? statuses)
    {
        var response = new ResponseModel<ICollection<TotalAmountByMonth>>();
        var rawTransactions = await transactionRepository.GetAllAsync(null, userId);
        year ??= DateTime.Now.Year;
        statuses = (statuses == null || statuses.Length == 0) ? new[] { "COMPLETED", "PENDING", "FAILED" } : statuses;
        var allMonths = Enum.GetValues(typeof(Months)).Cast<Months>().ToList();

        var query = rawTransactions.Where(t =>
            t.ScheduledAt.Year == year && statuses.Contains(t.TransactionStatus.ToString()));
        var groupedByMonth = query
            .GroupBy(t => t.ScheduledAt.Month)
            .OrderBy(g => g.Key)
            .Select(g => new TotalAmountByMonth
            {
                Month = ((Months)g.Key).ToString(),
                Paid = g.Where(t => t.TransactionType == TransactionType.PAY).Sum(t => t.Amount),
                Received = g.Where(t => t.TransactionType == TransactionType.RECEIVE).Sum(t => t.Amount),
            })
            .ToList();
        
        var result = allMonths.Select(month =>
        {
            var monthData = groupedByMonth.FirstOrDefault(m => m.Month == month.ToString());
            return new TotalAmountByMonth
            {
                Month = month.ToString(),
                Paid = monthData?.Paid ?? 0, 
                Received = monthData?.Received ?? 0 
            };
        }).ToList();

        return response.Ok(result, "Success" );;
    }

    public async Task<ResponseModel<TotalValue>> TotalEarned(Guid userId, int? year, string[]? statuses)
    {
        var response = new ResponseModel<TotalValue>();
        var rawTransactions = await transactionRepository.GetAllAsync(null, userId);
        year ??= DateTime.Now.Year;
        statuses = (statuses == null || statuses.Length == 0) ? new[] { "COMPLETED", "PENDING", "FAILED" } : statuses;

        var totalEarned = rawTransactions
            .Where(t => t.ScheduledAt.Year == year
                        && t.TransactionType == TransactionType.RECEIVE
                        && statuses.Contains(t.TransactionStatus.ToString()))
            .Sum(t => t.Amount);

        var dto = new TotalValue { Value = totalEarned };
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<TotalValue>> TotalSpent(Guid userId, int? year, string[]? statuses)
    {
        var response = new ResponseModel<TotalValue>();
        var rawTransactions = await transactionRepository.GetAllAsync(null, userId);
        year ??= DateTime.Now.Year;
        statuses = (statuses == null || statuses.Length == 0) ? new[] { "COMPLETED", "PENDING", "FAILED" } : statuses;

        var totalSpent = rawTransactions
            .Where(t => t.ScheduledAt.Year == year
                        && t.TransactionType == TransactionType.PAY
                        && statuses.Contains(t.TransactionStatus.ToString()))
            .Sum(t => t.Amount);

        var dto = new TotalValue { Value = totalSpent };
        return response.Ok(dto, "Success");;
    }

    public async Task<ResponseModel<HealthPercentage>> FinancialHealth(Guid userId, int? year, string[]? statuses)
    {
        var response = new ResponseModel<HealthPercentage>();
        year ??= DateTime.Now.Year;
        statuses = (statuses == null || statuses.Length == 0) ? new[] { "COMPLETED", "PENDING", "FAILED" } : statuses;

        var totalEarned = await TotalEarned(userId, year, statuses);
        var totalSpent = await TotalSpent(userId, year, statuses);
        var financialHealth = totalEarned.Data!.Value > 0 ? ((totalEarned.Data!.Value - totalSpent.Data!.Value) / totalEarned.Data!.Value) * 100 : 0;
        var result = float.Parse(financialHealth.ToString(CultureInfo.CurrentCulture));

        var dto = new HealthPercentage { Value = result };
        return response.Ok(dto, "Success");;
    }
}