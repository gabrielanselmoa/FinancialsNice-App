using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Address>> GetAllAsync(Guid userId)
    {
        return await _context.Addresses.Where(a => a.Status == Status.ACTIVE && a.OwnerId == userId).ToListAsync();
    }

    public async Task<Address?> GetByIdAsync(Guid? id)
    {
        return await _context.Addresses
            .Where(a=>a.Status == Status.ACTIVE)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task CreateAsync(Address address)
    {
        var result = _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(Address address)
    {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }
}