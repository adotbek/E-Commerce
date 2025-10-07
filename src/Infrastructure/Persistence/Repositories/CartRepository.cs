using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task<Cart> CreateAsync(Cart entity)
    {
        await context.Carts.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Cart?> GetByIdAsync(long id)
    {
        return await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cart?> GetByUserIdAsync(long userId)
    {
        return await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart> UpdateAsync(Cart entity)
    {
        context.Carts.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        if (cart is null)
            return false;

        context.Carts.Remove(cart);
        await context.SaveChangesAsync();
        return true;
    }
}
