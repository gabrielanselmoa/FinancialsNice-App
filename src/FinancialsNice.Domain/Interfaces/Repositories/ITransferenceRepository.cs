using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface ITransferenceRepository
{
    Task<ICollection<Transference>> GetAllAsync(Guid goalId);
    Task<Transference?> GetByIdAsync(Guid id, Guid goalId);
    Task CreateAsync(Transference transference);
    Task UpdateAsync(Guid id, Transference transference);
    Task SoftDeleteAsync(Transference transference);
    Task HardDeleteAsync(Transference transference);
}