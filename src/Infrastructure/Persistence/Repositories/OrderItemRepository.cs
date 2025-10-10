using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderItemRepository(AppDbContext context) : IOrderItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.OrderItems
            .Include(x => x.Product)
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.OrderItems
            .Include(x => x.Product)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<OrderItem> AddAsync(OrderItem entity, CancellationToken cancellationToken = default)
    {
        await _context.OrderItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<OrderItem> UpdateAsync(OrderItem entity, CancellationToken cancellationToken = default)
    {
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is not null)
        {
            _context.OrderItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
