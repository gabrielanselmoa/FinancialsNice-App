using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IPayerReceiverRepository {
    
    Task<PayerReceiver?> GetByIdAsync (Guid id);
    Task<PayerReceiver?> GetByNameAsync (string? name);
    Task<ICollection<PayerReceiver>> GetAllBySearchAsync(string? search, UserType? type, Guid userId);
    Task CreateAsync (PayerReceiver payerReceiver);
    Task UpdateAsync (Guid id, PayerReceiver payerReceiver);
    Task SoftDeleteAsync (PayerReceiver payerReceiver);
    Task HardDeleteAsync (PayerReceiver payerReceiver);
}