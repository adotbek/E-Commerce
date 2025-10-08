using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartItemRepository(AppDbContext context) : ICartItemRepository
{
    public async Task<CartItem> CreateAsync(CartItem entity)
    {
        await context.CartItems.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<CartItem?> GetByIdAsync(long id)
    {
        return await context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public async Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId)
    {
        return await context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.CartId == cartId)
            .ToListAsync();
    }

    public async Task<CartItem> UpdateAsync(CartItem entity)
    {
        context.CartItems.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await context.CartItems.FirstOrDefaultAsync(ci => ci.Id == id);
        if (entity is null)
            return false;

        context.CartItems.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
