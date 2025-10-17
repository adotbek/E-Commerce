using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Cart entity)
    {
        await _context.Carts.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Cart?> GetByIdAsync(long id)
    {
        return await _context.Carts
            .Include(c => c.User)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cart?> GetByUserIdAsync(long userId)
    {
        return await _context.Carts
            .Include(c => c.User)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task UpdateAsync(Cart entity)
    {
        _context.Carts.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.Carts.FindAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Cart with Id={id} not found.");

        _context.Carts.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _context.Carts.AnyAsync(c => c.UserId == userId);
    }

    public async Task<decimal> CalculateTotalPriceAsync(long cartId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null)
            throw new KeyNotFoundException($"Cart with Id={cartId} not found.");

        var total = cart.Items?.Sum(i => i.UnitPrice * i.Quantity) ?? 0;
        cart.TotalPrice = total;

        await _context.SaveChangesAsync();
        return total;
    }

    public async Task ClearCartAsync(long cartId)
    {
        var items = _context.CartItems.Where(i => i.CartId == cartId);
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
