using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Payment>> GetAllAsync(Guid userId)
        {
            return await _context.Payments
                .Where(p => p.Status == Status.ACTIVE && p.OwnerId == userId)
                .Include(p => p.Card)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Payments
                .Where(p=>p.Status == Status.ACTIVE && p.OwnerId == userId)
                .Include(p => p.Card)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync (Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
        
        public async Task HardDeleteAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}