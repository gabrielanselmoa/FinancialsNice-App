using FinancialsNice.Application.Dtos.Permissions;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IPermissionService
{
    Task<ResponseModel<ICollection<PermissionResponse>>> GetAllAsync();
    Task<ResponseModel<PermissionResponse?>> GetByIdAsync(int id);
    Task<ResponseModel<PermissionResponse?>> GetByNameAsync(PermissionRequest request);
    Task<ResponseModel<PermissionResponse?>> CreateAsync(PermissionRequest request);
    Task<ResponseModel<PermissionResponse?>> UpdateAsync(int id, PermissionUpdate update);
    Task<ResponseModel<bool>> SoftDeleteAsync(int id);
    Task<ResponseModel<bool>> HardDeleteAsync(int id);
}