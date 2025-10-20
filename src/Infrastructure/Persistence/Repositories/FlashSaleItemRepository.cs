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

    public async Task<long> AddAsync(FlashSaleItem entity)
    {
        await _context.FlashSaleItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
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
            throw new KeyNotFoundException($"FlashSaleItem with Id={id} not found.");

        _context.FlashSaleItems.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<FlashSaleItem>> GetByFlashSaleIdAsync(long flashSaleId)
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .Where(f => f.FlashSaleId == flashSaleId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<FlashSaleItem?> GetByProductIdAsync(long productId)
    {
        return await _context.FlashSaleItems
            .Include(f => f.FlashSale)
            .Include(f => f.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.ProductId == productId);
    }

    public async Task<IEnumerable<FlashSaleItem>> GetActiveItemsAsync(DateTime now)
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .Where(f => f.FlashSale.StartTime<= now && f.FlashSale.EndTime>= now)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> CalculateDiscountedPriceAsync(long productId, long flashSaleId)
    {
        var item = await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .FirstOrDefaultAsync(f => f.ProductId == productId && f.FlashSaleId == flashSaleId);

        if (item is null)
            throw new KeyNotFoundException("Flash sale item not found.");

        var productPrice = item.Product.Price;
        var discountPercent = item.FlashSale.DiscountedPrice;


        var discountedPrice = productPrice - (productPrice * (discountPercent / 100));

        return discountedPrice;
    }



    public async Task<bool> ExistsAsync(long productId, long flashSaleId)
    {
        return await _context.FlashSaleItems
            .AnyAsync(f => f.ProductId == productId && f.FlashSaleId == flashSaleId);
    }

    public async Task<int> RemoveExpiredItemsAsync(DateTime now)
    {
        var expired = await _context.FlashSaleItems
            .Include(f => f.FlashSale)
            .Where(f => f.FlashSale.EndTime < now)
            .ToListAsync();

        _context.FlashSaleItems.RemoveRange(expired);
        return await _context.SaveChangesAsync();
    }
}
