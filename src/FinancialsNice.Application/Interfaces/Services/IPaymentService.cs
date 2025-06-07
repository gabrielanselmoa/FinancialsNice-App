using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PagedResponseModel<ICollection<PaymentResponse>>> GetAllAsync(int page, int pageSize, Guid userId);
        Task<ResponseModel<PaymentResponse?>> GetByIdAsync(Guid id, Guid userId);
        Task<ResponseModel<PaymentResponse>> CreateAsync(PaymentRequest request, Guid userId);
        Task<ResponseModel<PaymentResponse>> UpdateAsync(Guid id, Guid userId, PaymentUpdate update);
        Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
        Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
    }
}