namespace FinancialsNice.Application.Interfaces.Services;

public interface IReportService
{
    public Task<byte[]> GenerateReport(Guid userId);
}