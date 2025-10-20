using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlashSaleRepository : IFlashSaleRepository
{
    private readonly AppDbContext _context;

    public FlashSaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(FlashSale entity)
    {
        await _context.FlashSales.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<FlashSale?> GetByIdAsync(long id)
    {
        return await _context.FlashSales
            .Include(f => f.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<FlashSale>> GetAllAsync()
    {
        return await _context.FlashSales
            .Include(f => f.Items)
            .ThenInclude(i => i.Product)
            .OrderByDescending(f => f.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<FlashSale>> GetActiveAsync(DateTime at)
    {
        return await _context.FlashSales
            .Include(f => f.Items)
            .ThenInclude(i => i.Product)
            .Where(f => f.StartTime <= at && f.EndTime >= at)
            .ToListAsync();
    }

    public async Task UpdateAsync(FlashSale entity)
    {
        _context.FlashSales.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.FlashSales.FindAsync(id);
        if (existing is null)
            return;

        _context.FlashSales.Remove(existing);
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<FlashSale>> GetActiveAsync(DateTime? at = null)
    {
        var now = at ?? DateTime.UtcNow;
        return await GetActiveAsync(now);
    }

    public async Task<int> RemoveExpiredAsync(DateTime? now = null)
    {
        var date = now ?? DateTime.UtcNow;
        var expiredSales = await _context.FlashSales
            .Where(f => f.EndTime < date)
            .ToListAsync();

        if (!expiredSales.Any())
            return 0;

        _context.FlashSales.RemoveRange(expiredSales);
        return await _context.SaveChangesAsync();
    }

    public async Task<FlashSale?> GetActiveByProductIdAsync(long productId)
    {
        var now = DateTime.UtcNow;

        return await _context.FlashSales
            .Include(f => f.Items)
            .ThenInclude(i => i.Product)
            .Where(f => f.StartTime <= now && f.EndTime >= now)
            .FirstOrDefaultAsync(f => f.Items.Any(i => i.ProductId == productId));
    }

    public async Task<bool> IsActiveAsync(long flashSaleId, DateTime? now = null)
    {
        var date = now ?? DateTime.UtcNow;

        return await _context.FlashSales
            .AnyAsync(f => f.Id == flashSaleId &&
                           f.StartTime <= date &&
                           f.EndTime >= date);
    }
}
