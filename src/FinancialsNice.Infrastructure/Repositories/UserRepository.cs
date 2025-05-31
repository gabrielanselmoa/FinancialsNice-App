using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await context.Users
            .Include(u => u.Address)
            .Include(u => u.Cards)
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Include(u => u.Wallet)
            .Include(u => u.Goals)
            .Where(u=>u.Status == Status.ACTIVE)
            .FirstOrDefaultAsync(u=> u.Id == id);
        return user;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
       return await context.Users
           .Include(u => u.Address)
           .Include(u => u.Cards)
           .Include(u => u.Roles!)
           .ThenInclude(r => r.Permissions)
           .Include(u => u.Wallet)
           .Include(u => u.Goals)
           .Where(u => u.Status == Status.ACTIVE)
           .ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await context.Users
            .Include(u => u.Address)
            .Include(u => u.Cards)
            .Include(u => u.Roles!)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Status == Status.ACTIVE)
            .FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
    
    public async Task HardDeleteAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}