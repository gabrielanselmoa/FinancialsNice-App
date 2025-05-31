using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories
{
    public class PayerReceiverRepository : IPayerReceiverRepository
    {
        private readonly ApplicationDbContext _context;

        public PayerReceiverRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PayerReceiver?> GetByIdAsync(Guid id)
        {
            return await _context.PayerReceivers
                .Where(pr => pr.Status == Status.ACTIVE)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<PayerReceiver?> GetByNameAsync(string? name)
        {
            return await _context.PayerReceivers.FirstOrDefaultAsync(pr => pr.Name == name);
        }

        public async Task<ICollection<PayerReceiver>> GetAllBySearchAsync(string? search, UserType? type, Guid userId)
        {
            if (search == null)
                return await _context.PayerReceivers.Where(pr => pr.Status == Status.ACTIVE && pr.OwnerId == userId)
                .ToListAsync();
            
            return await _context.PayerReceivers.Where(pr => pr.Status == Status.ACTIVE && (pr.Name.Contains(search) || 
                pr.Description.Contains
                (search) || pr.Type.Equals(type)) && pr.OwnerId == userId).ToListAsync();
        }

        public async Task CreateAsync(PayerReceiver payerReceiver)
        {
            _context.PayerReceivers.Add(payerReceiver);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, PayerReceiver payerReceiver)
        {
            _context.PayerReceivers.Update(payerReceiver);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(PayerReceiver payerReceiver)
        {
            _context.PayerReceivers.Update(payerReceiver);
            await _context.SaveChangesAsync();
        }

        public async Task HardDeleteAsync(PayerReceiver payerReceiver)
        {
            _context.PayerReceivers.Remove(payerReceiver);
            await _context.SaveChangesAsync();
        }
    }
}