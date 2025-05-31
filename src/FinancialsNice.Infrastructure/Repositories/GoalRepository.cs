using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly ApplicationDbContext _context;

    public GoalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Goal>> GetAllAsync(Guid userId, string? search)
    {
        if (search == null)
            return await _context.Goals
                .Where(g => g.OwnerId == userId && g.Status == Status.ACTIVE).ToListAsync();
        
        return await _context.Goals.Where(g => g.OwnerId == userId && g.Status == Status.ACTIVE && g.Name.ToLower().Contains(search.ToLower()) ).ToListAsync();
    }

    public async Task<Goal?> GetByIdAsync(Guid? id)
    {
        return await _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.Status == Status.ACTIVE);
    }

    public async Task CreateAsync(Goal goal)
    {
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Goal goal)
    {
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();
    }
    
    public async Task SoftDeleteAsync(Goal goal)
    {
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();
    }
    
    public async Task HardDeleteAsync(Goal goal)
    {
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
    }
}