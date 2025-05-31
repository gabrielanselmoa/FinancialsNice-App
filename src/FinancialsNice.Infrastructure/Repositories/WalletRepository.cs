using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class WalletRepository(ApplicationDbContext context) : IWalletRepository
{
    public async Task<Wallet> GetAsync(Guid userId)
    {
        return (await context.Wallets.FirstOrDefaultAsync(w => w != null && w.OwnerId == userId && w.Status == Status.ACTIVE))!;
    }
}