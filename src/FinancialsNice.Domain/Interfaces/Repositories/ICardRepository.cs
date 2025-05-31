using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface ICardRepository {
    
    Task<ICollection<Card>> GetAllAsync(Guid userId);
    Task<Card?> GetByIdAsync(Guid id, Guid userId);
    Task<Card?> GetByNameAsync(string name, Guid userId);
    Task<Card?> GetByNumberAsync(string number, Guid userId);
    Task CreateAsync (Card card);
    Task UpdateAsync (Guid id, Card card);
    Task SoftDeleteAsync (Card card);
    Task HardDeleteAsync (Card card);
}