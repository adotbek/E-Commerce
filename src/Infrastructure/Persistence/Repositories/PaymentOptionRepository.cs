using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentOptionRepository(AppDbContext context) : IPaymentOptionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PaymentOption>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PaymentOptions.Include(p => p.User).ToListAsync(cancellationToken);
    }

    public async Task<PaymentOption?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentOptions.Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PaymentOption> AddAsync(PaymentOption entity, CancellationToken cancellationToken = default)
    {
        await _context.PaymentOptions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<PaymentOption> UpdateAsync(PaymentOption entity, CancellationToken cancellationToken = default)
    {
        _context.PaymentOptions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.PaymentOptions.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _context.PaymentOptions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
