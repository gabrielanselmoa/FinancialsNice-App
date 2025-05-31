using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Where(r => r.Status == Status.ACTIVE)
            .Include(r => r.Permissions)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .Where(r => r.Status == Status.ACTIVE)
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id.Equals(id));
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .Where(r => r.Status == Status.ACTIVE)
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Role> SeedDatabase()
    {
        var role = new Role()
        {
            Name = "user-default",
            Permissions =
            [
                new Permission()
                {
                    Name = "/transactions/overview",
                    SlugName = "transactions-overview"
                },
                new Permission()
                {
                    Name = "/transactions/form",
                    SlugName = "transactions-form"
                },
                new Permission()
                {
                    Name = "/payerOrReceiver",
                    SlugName = "payerOrReceiver"
                },
                new Permission()
                {
                    Name = "/card/new",
                    SlugName = "card-new"
                },
                new Permission()
                {
                    Name = "download_invoices",
                    SlugName = "download-invoices"
                }
            ]
        };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task CreateAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }
    
    public async Task HardDeleteAsync(Role role)
    {
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
}