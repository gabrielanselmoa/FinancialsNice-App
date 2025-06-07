using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{

    // Criar queries personalizadas para pegar transactions por qualquer info
    Task<ICollection<Transaction>> GetAllAsync (string? search, Guid? userId);
    Task<ICollection<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync (Guid id);
    Task CreateAsync (Transaction transaction);
    Task UpdateAsync (Guid id, Transaction transaction);
    Task SoftDeleteAsync (Transaction transaction);
    Task HardDeleteAsync (Transaction transaction);
}