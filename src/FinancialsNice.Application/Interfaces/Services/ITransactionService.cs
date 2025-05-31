using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Transactions;

namespace FinancialsNice.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<PagedResponseModel<ICollection<TransactionResponse>>> GetAllAsync(string? search, string? startDate, string? endDate, int page, int perPage, Guid userId);
        Task<ResponseModel<TransactionResponse?>> GetByIdAsync(Guid id, Guid userId);
        Task<ResponseModel<ICollection<TransactionPreview>>> GetLastAsync(Guid userId);
        Task<ResponseModel<TransactionResponse>> CreateAsync(TransactionRequest request, Guid userId);
        Task<ResponseModel<TransactionResponse>> UpdateAsync(Guid id, Guid userId, TransactionUpdate update);
        Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
        Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
    }
}