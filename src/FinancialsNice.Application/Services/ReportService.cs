using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class ReportService(ITransactionRepository transactionRepository, IPdfGenerator generator)
    : IReportService
{
    private readonly IPdfGenerator _generator = generator;

    public async Task<byte[]> GenerateReport(Guid userid)
    {
        var transactions = await transactionRepository.GetAllAsync(null, userid);
        if (!transactions.Any()) return [];
        
        var dto = transactions.Select(TransactionMapper.ToResponse).ToList();
        var pdfBytes = _generator.GenerateTransactionReport(dto);
        return pdfBytes;
    }
}