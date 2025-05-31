using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Ticket>> GetAllAsync(Guid userId)
    {
        return await _context.Tickets
            .Where(t => t.ClientId == userId)
            .Include(t => t.Client)
            .Include(t => t.ResolvedBy)
            .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Tickets
            .Where(t => t.Id == id && t.ClientId == userId)
            .Include(t => t.Client)
            .Include(t => t.ResolvedBy != null)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }
    
    public async Task HardDeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}