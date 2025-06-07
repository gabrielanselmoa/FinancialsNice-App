using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IGoalService
{
    Task<PagedResponseModel<ICollection<GoalResponse>>> GetAllAsync(int page, int perPage, Guid userId, string? search);
    Task<ResponseModel<GoalResponse?>> GetByIdAsync(Guid id, Guid userId);
    Task<ResponseModel<GoalResponse?>> CreateAsync(GoalRequest request, Guid userId);
    Task<ResponseModel<GoalResponse?>> UpdateAsync(Guid id, Guid userId, GoalUpdate update);
    Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
    Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
}
