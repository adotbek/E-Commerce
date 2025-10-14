using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlashSaleItemRepository : IFlashSaleItemRepository
{
    private readonly AppDbContext _context;

    public FlashSaleItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlashSaleItem>> GetAllAsync()
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<FlashSaleItem?> GetByIdAsync(long id)
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddAsync(FlashSaleItem entity)
    {
        await _context.FlashSaleItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FlashSaleItem entity)
    {
        _context.FlashSaleItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.FlashSaleItems.FindAsync(id);
        if (existing is null)
            return;

        _context.FlashSaleItems.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
