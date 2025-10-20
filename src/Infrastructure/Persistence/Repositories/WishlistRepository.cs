using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Wishlist>> GetAllAsync()
    {
        return await _context.Wishlists
            .Include(x => x.Items)!
            .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<Wishlist?> GetByIdAsync(long id)
    {
        return await _context.Wishlists
            .Include(x => x.Items)!
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Wishlist entity)
    {
        await _context.Wishlists.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wishlist entity)
    {
        _context.Wishlists.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Wishlist entity)
    {
        _context.Wishlists.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Wishlist?> GetByUserIdAsync(long userId)
    {
        return await _context.Wishlists
            .Include(x => x.Items)!
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _context.Wishlists.AnyAsync(x => x.UserId == userId);
    }

    public async Task<int> GetItemCountAsync(long wishlistId)
    {
        return await _context.WishlistItems
            .CountAsync(x => x.WishlistId == wishlistId);
    }

    public async Task ClearAsync(long wishlistId)
    {
        var items = await _context.WishlistItems
            .Where(x => x.WishlistId == wishlistId)
            .ToListAsync();

        if (items.Count == 0)
            return;

        _context.WishlistItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
