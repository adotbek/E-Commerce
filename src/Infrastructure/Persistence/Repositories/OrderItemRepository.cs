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

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(long orderId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<decimal> CalculateTotalAsync(long orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .SumAsync(oi => oi.Quantity * oi.UnitPrice);
    }

    public async Task<bool> ExistsAsync(long orderItemId)
    {
        return await _context.OrderItems.AnyAsync(oi => oi.Id == orderItemId);
    }

    public async Task<int> GetTotalQuantityAsync(long orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .SumAsync(oi => oi.Quantity);
    }

    public async Task AddOrUpdateItemAsync(long orderId, long productId, int quantity)
    {
        var existing = await _context.OrderItems
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

        if (existing is not null)
        {
            existing.Quantity += quantity;
            _context.OrderItems.Update(existing);
        }
        else
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            var newItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.DiscountPrice ?? product.Price
            };

            await _context.OrderItems.AddAsync(newItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteByOrderIdAsync(long orderId)
    {
        var items = await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();

        _context.OrderItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsInOrderAsync(long orderId, long productId)
    {
        return await _context.OrderItems
            .AnyAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
    }
}
