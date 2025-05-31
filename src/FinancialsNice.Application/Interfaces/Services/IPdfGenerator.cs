using FinancialsNice.Application.Dtos.Transactions;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IPdfGenerator
{
    public byte[] GenerateTransactionReport(List<TransactionResponse> transactions);
}