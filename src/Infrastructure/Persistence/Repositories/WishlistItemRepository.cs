using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WishlistItemRepository : IWishlistItemRepository
{
    private readonly AppDbContext _context;

    public WishlistItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WishlistItem>> GetAllAsync()
    {
        return await _context.WishlistItems
            .Include(i => i.Product)
            .Include(i => i.Wishlist)
            .ToListAsync();
    }

    public async Task<WishlistItem?> GetByIdAsync(long id)
    {
        return await _context.WishlistItems
            .Include(i => i.Product)
            .Include(i => i.Wishlist)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddAsync(WishlistItem entity)
    {
        await _context.WishlistItems.AddAsync(entity);
    }

    public async Task UpdateAsync(WishlistItem entity)
    {
        _context.WishlistItems.Update(entity);
    }

    public async Task DeleteAsync(WishlistItem entity)
    {
        _context.WishlistItems.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
