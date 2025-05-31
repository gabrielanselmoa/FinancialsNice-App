using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<ICollection<Payment>> GetAllAsync (Guid userId);
        Task<Payment?> GetByIdAsync (Guid id, Guid userId);
        Task CreateAsync (Payment payment);
        Task UpdateAsync (Payment payment);
        Task SoftDeleteAsync (Payment payment);
        Task HardDeleteAsync (Payment payment);
    }
}