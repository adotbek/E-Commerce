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
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WishlistItem entity)
    {
        _context.WishlistItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(WishlistItem entity)
    {
        _context.WishlistItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserIdAsync(long userId)
    {
        return await _context.WishlistItems
            .Include(i => i.Product)
            .Include(i => i.Wishlist)
            .Where(i => i.Wishlist.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(long wishlistId)
    {
        return await _context.WishlistItems
            .Include(i => i.Product)
            .Where(i => i.WishlistId == wishlistId)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(long wishlistId, long productId)
    {
        return await _context.WishlistItems
            .AnyAsync(i => i.WishlistId == wishlistId && i.ProductId == productId);
    }

    public async Task<int> GetCountByWishlistIdAsync(long wishlistId)
    {
        return await _context.WishlistItems
            .CountAsync(i => i.WishlistId == wishlistId);
    }

    public async Task ClearWishlistAsync(long wishlistId)
    {
        var items = _context.WishlistItems.Where(i => i.WishlistId == wishlistId);
        _context.WishlistItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
    