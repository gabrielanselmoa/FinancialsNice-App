using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IGoalRepository
{
    Task<ICollection<Goal>> GetAllAsync(Guid userId, string? search);
    Task<Goal?> GetByIdAsync(Guid? id);
    Task CreateAsync(Goal goal);
    Task UpdateAsync(Guid id, Goal goal);
    Task SoftDeleteAsync(Goal goal);
    Task HardDeleteAsync(Goal goal);
}