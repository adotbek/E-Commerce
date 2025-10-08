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

    public async Task<FlashSale> CreateAsync(FlashSale entity)
    {
        await _context.FlashSales.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<FlashSale?> GetByIdAsync(long id)
    {
        return await _context.FlashSales.FindAsync(id);
    }

    public async Task<IEnumerable<FlashSale>> GetAllAsync()
    {
        return await _context.FlashSales
            .OrderByDescending(f => f.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<FlashSale>> GetActiveAsync(DateTime at)
    {
        return await _context.FlashSales
            .Where(f => f.StartTime <= at && f.EndTime >= at)
            .OrderBy(f => f.EndTime)
            .ToListAsync();
    }

    public async Task<FlashSale> UpdateAsync(FlashSale entity)
    {
        _context.FlashSales.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.FlashSales.FindAsync(id);
        if (entity is null) return false;

        _context.FlashSales.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
