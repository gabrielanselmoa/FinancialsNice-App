using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Transaction>> GetAllAsync(string? search, Guid?
        userId)
    {
        if (search == null || String.IsNullOrWhiteSpace(search))
        {
            return await _context.Transactions
                .Include(t => t.PayerReceiver)
                .Include(t => t.Payments)
                .ThenInclude(t => t.Card)
                .Where(t => t.Status == Status.ACTIVE && t.OwnerId == userId)
                .ToListAsync();
        }

        return await _context.Transactions
            .Where(t => t.Status == Status.ACTIVE &&
                        (t.Description.ToLower().Contains(search) ||
                         t.Email.ToLower().Contains(search) ||
                         t.Code.ToLower().Contains(search) ||
                         t.Currency.ToLower().Contains(search)
                        ) && t.OwnerId == userId)
            .Include(t => t.PayerReceiver)
            .Include(t => t.Payments)
            .ThenInclude(t => t.Card)
            .ToListAsync();
    }
    public async Task<ICollection<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.PayerReceiver)
            .Include(t => t.Payments)
            .ThenInclude(t => t.Card)
            .Where(t => t.Status == Status.ACTIVE)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions
            .Where(t => t.Status == Status.ACTIVE)
            .Include(t => t.PayerReceiver)
            .Include(t => t.Payments)
            .ThenInclude(t => t.Card)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    public async Task CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task HardDeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}