using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly ApplicationDbContext _context;

    public CardRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Card>> GetAllAsync(Guid userId)
    {
        var cards = await _context.Cards
            .Where(c => c.Status == Status.ACTIVE && c.OwnerId == userId)
            .ToListAsync();
        return cards;
    }

    public async Task<Card?> GetByIdAsync(Guid id, Guid userId)
    {
        var card = await _context.Cards
            .Where(c=>c.Status == Status.ACTIVE && c.OwnerId == userId)
            .FirstOrDefaultAsync(c => c.Id == id);
        return card;
    }

    public async Task<Card?> GetByNameAsync(string name, Guid userId)
    {
        var card = await _context.Cards.FirstOrDefaultAsync(c => c.Name == name && c.OwnerId == userId);
        return card;
    }

    public async Task<Card?> GetByNumberAsync(string number, Guid userId)
    {
        var card = await _context.Cards.FirstOrDefaultAsync(c => c.Number == number && c.OwnerId == userId);
        return card;
    }

    public async Task CreateAsync(Card card)
    {
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Card card)
    {
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Card card)
    {
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }
    
    public async Task HardDeleteAsync(Card card)
    {
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }
}