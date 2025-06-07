using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class TransferenceRepository : ITransferenceRepository
{
    private readonly ApplicationDbContext _context;

    public TransferenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Transference>> GetAllAsync(Guid goalId)
    {
        return await _context.Transferences
            .Where(t => t.GoalId == goalId)
            .ToListAsync();
    }

    public async Task<Transference?> GetByIdAsync(Guid id, Guid goalId)
    {
        return await _context.Transferences
            .FirstOrDefaultAsync(t => t.Id == id && t.GoalId == goalId);
    }

    public async Task CreateAsync(Transference transference)
    {
        _context.Transferences.Add(transference);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Transference transference)
    {
        _context.Transferences.Update(transference);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Transference transference)
    {
        _context.Transferences.Update(transference);
        await _context.SaveChangesAsync();
    }

    public async Task HardDeleteAsync(Transference transference)
    {
        _context.Transferences.Remove(transference);
        await _context.SaveChangesAsync();
    }
}