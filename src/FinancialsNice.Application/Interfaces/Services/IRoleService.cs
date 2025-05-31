using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Roles;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IRoleService
{
    Task<ResponseModel<ICollection<RoleResponse>>> GetAllAsync();
    Task<ResponseModel<RoleResponse?>> GetByIdAsync(int id);
    Task<ResponseModel<RoleResponse?>> GetByNameAsync(RoleRequest request);
    Task<ResponseModel<RoleResponse?>> CreateAsync(RoleRequest request);
    Task<ResponseModel<RoleResponse?>> UpdateAsync(int id, RoleUpdate update);
    Task<ResponseModel<bool>> SoftDeleteAsync(int id);
    Task<ResponseModel<bool>> HardDeleteAsync(int id);
}