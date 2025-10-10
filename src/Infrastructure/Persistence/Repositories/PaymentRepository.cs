using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository(AppDbContext context) : IPaymentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Payments.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _context.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        _context.Payments.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Payment> UpdateAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        _context.Payments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Payments.FindAsync([id], cancellationToken);
        if (entity is not null)
        {
            _context.Payments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
