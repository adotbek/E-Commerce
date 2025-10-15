using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(CartItem entity)
    {
        await _context.CartItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<CartItem?> GetByIdAsync(long id)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public async Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.CartId == cartId)
            .ToListAsync();
    }

    public async Task UpdateAsync(CartItem entity)
    {
        _context.CartItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _context.CartItems.FindAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Cart item with Id={id} not found.");

        _context.CartItems.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
