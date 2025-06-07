using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IAddressService
{
    Task<PagedResponseModel<ICollection<AddressResponse>>> GetAllAsync(int page, int perPage, Guid userId);
    Task<ResponseModel<AddressResponse?>> GetByIdAsync(Guid id, Guid userId);
    Task<ResponseModel<AddressResponse?>> CreateAsync(AddressRequest request, Guid userId);
    Task<ResponseModel<AddressResponse?>> UpdateAsync(Guid id, Guid userId, AddressUpdate update);
    Task<ResponseModel<bool>> SoftDeleteAsync(Guid id,  Guid userId);
    Task<ResponseModel<bool>> HardDeleteAsync(Guid id,  Guid userId);
}