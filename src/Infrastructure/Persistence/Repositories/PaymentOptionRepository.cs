using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentOptionRepository (AppDbContext _context) : IPaymentOptionRepository
{  

    public async Task<IEnumerable<PaymentOption>> GetAllAsync()
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PaymentOption?> GetByIdAsync(long id)
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> AddAsync(PaymentOption entity)
    {
        await _context.PaymentOptions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(PaymentOption entity)
    {
        _context.PaymentOptions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.PaymentOptions.FindAsync(id);
        if (existing is null)
            return;

        _context.PaymentOptions.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
