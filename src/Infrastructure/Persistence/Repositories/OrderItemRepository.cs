using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(long id)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }

    public async Task<long> AddAsync(OrderItem entity)
    {
        await _context.OrderItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(OrderItem entity)
    {
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.OrderItems.FindAsync(id);
        if (existing is null)
            return;

        _context.OrderItems.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
