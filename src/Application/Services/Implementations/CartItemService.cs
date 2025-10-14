using Application.Interfaces.Repositories;
using Domain.Entities;

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
            .Include(ci => ci.Cart)
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
        var item = await _context.CartItems.FindAsync(id);
        if (item is null)
            throw new KeyNotFoundException($"CartItem with Id={id} not found.");

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}
