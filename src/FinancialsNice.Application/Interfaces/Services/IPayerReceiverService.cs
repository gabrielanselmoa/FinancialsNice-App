using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Interfaces.Services
{
    public interface IPayerReceiverService
    {
        Task<PagedResponseModel<ICollection<PayerReceiverResponse>>> GetAllBySearchAsync(string? search, UserType? type, int page, int perPage, Guid userId);
        Task<ResponseModel<PayerReceiverResponse?>> GetByIdAsync(Guid id, Guid userId);
        Task<ResponseModel<PayerReceiverResponse>> CreateAsync(PayerReceiverRequest request, Guid userId);
        Task<ResponseModel<PayerReceiverResponse>> UpdateAsync(Guid id, Guid userId, PayerReceiverUpdate update);
        Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
        Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
    }
}