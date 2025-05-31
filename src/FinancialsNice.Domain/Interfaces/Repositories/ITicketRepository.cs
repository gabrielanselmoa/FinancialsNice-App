using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<ICollection<Ticket>> GetAllAsync(Guid userId);
    Task<Ticket?> GetByIdAsync(Guid id, Guid userId);
    Task CreateAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
    Task SoftDeleteAsync(Ticket ticket);
    Task HardDeleteAsync(Ticket ticket);
}