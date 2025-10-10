// Infrastructure/Repositories/OrderRepository.cs
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Orders
                         .Include(o => o.Items)
                         .ToListAsync(cancellationToken);

    public async Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _context.Orders
                         .Include(o => o.Items)
                         .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task<Order> AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Orders.FindAsync([id], cancellationToken);
        if (entity is not null)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
