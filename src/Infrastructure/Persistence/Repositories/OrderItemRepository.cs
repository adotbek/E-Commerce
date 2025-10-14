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
            .OrderByDescending(o => o.Id)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(long id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task<OrderItem> AddAsync(OrderItem entity)
    {
        await _context.OrderItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem> UpdateAsync(OrderItem entity)
    {
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.OrderItems.FindAsync(id);
        if (entity is null) return false;

        _context.OrderItems.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
