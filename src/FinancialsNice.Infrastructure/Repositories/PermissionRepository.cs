using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Permission>> GetAllAsync()
        {
            return await _context.Permissions.Where(p => p.Status == Status.ACTIVE).ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions
                .Where(p => p.Status == Status.ACTIVE)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public Task<Permission?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }
        
        public async Task HardDeleteAsync(Permission permission)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }
}