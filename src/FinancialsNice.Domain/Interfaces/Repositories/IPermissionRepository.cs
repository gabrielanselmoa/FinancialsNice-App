using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;
    public interface IPermissionRepository
    {
        Task<ICollection<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission?> GetByNameAsync(string name);
        Task CreateAsync(Permission permission);
        Task UpdateAsync(int id, Permission permission);
        Task SoftDeleteAsync (Permission permission );
        Task HardDeleteAsync (Permission permission);
    }