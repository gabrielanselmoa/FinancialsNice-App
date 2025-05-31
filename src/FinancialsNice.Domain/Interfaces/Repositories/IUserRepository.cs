using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IUserRepository {
    
    Task<ICollection<User>> GetAllAsync();
    Task<User?> GetByIdAsync (Guid id);
    Task<User?> GetByEmailAsync (string email);
    Task CreateAsync (User user);
    Task UpdateAsync (Guid id, User user);
    Task SoftDeleteAsync (User user);
    Task HardDeleteAsync (User user);
}