using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Users;

namespace FinancialsNice.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<PagedResponseModel<ICollection<UserResponse>>> GetAllAsync(int page, int perPage);
        Task<ResponseModel<UserResponse?>> GetByIdAsync(Guid id);
        Task<ResponseModel<UserResponse?>> GetByLoggedUserIdAsync(Guid id);
        Task<ResponseModel<UserResponse?>> GetByEmailAsync(string email);
        Task<ResponseModel<UserResponse>> UpdateAsync(Guid userId, UserUpdate updateUser);
        Task<ResponseModel<bool>> SoftDeleteAsync(Guid id);
        Task<ResponseModel<bool>> HardDeleteAsync(Guid id);
    }
}