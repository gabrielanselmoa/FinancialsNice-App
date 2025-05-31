using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Domain.Interfaces.Repositories;

public interface IWalletRepository
{
    Task<Wallet> GetAsync(Guid userId);
}