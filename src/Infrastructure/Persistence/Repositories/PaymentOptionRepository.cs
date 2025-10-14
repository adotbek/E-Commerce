using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentOptionRepository : IPaymentOptionRepository
{
    private readonly AppDbContext _context;

    public PaymentOptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaymentOption>> GetAllAsync()
    {
        return await _context.PaymentOptions
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    public async Task<PaymentOption?> GetByIdAsync(long id)
    {
        return await _context.PaymentOptions.FindAsync(id);
    }

    public async Task<PaymentOption> AddAsync(PaymentOption entity)
    {
        await _context.PaymentOptions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PaymentOption> UpdateAsync(PaymentOption entity)
    {
        _context.PaymentOptions.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.PaymentOptions.FindAsync(id);
        if (entity is null) return false;

        _context.PaymentOptions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
