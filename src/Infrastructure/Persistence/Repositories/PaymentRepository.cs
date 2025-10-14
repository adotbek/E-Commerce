using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(long id)
    {
        return await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> AddAsync(Payment entity)
    {
        await _context.Payments.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Payment entity)
    {
        _context.Payments.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.Payments.FindAsync(id);
        if (existing is null)
            return;

        _context.Payments.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
