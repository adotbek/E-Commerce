using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

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
}
