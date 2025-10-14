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


}
