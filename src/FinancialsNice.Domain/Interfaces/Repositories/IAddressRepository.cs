using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IAddressRepository {
    
    Task<ICollection<Address>> GetAllAsync(Guid userId);
    Task<Address?> GetByIdAsync(Guid? id);
    Task CreateAsync (Address address);
    Task UpdateAsync (Guid id, Address address);
    Task SoftDeleteAsync (Address address);
    Task HardDeleteAsync (Address address);
}