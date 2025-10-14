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
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(long id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<Payment> AddAsync(Payment entity)
    {
        await _context.Payments.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Payment> UpdateAsync(Payment entity)
    {
        _context.Payments.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.Payments.FindAsync(id);
        if (entity is null) return false;

        _context.Payments.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
