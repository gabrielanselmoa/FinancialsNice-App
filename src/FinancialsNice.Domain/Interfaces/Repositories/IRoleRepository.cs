using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<ICollection<Role>> GetAllAsync ();
    Task<Role?> GetByIdAsync (int id);
    Task<Role?> GetByNameAsync (string name);
    Task<Role> SeedDatabase();
    Task CreateAsync(Role role);
    Task UpdateAsync (int id, Role role);
    Task SoftDeleteAsync (Role role);
    Task HardDeleteAsync (Role role);
}
