namespace Infrastructure.Repositories;

using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class FlashSaleItemRepository(AppDbContext context) : IFlashSaleItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<FlashSaleItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .ToListAsync(cancellationToken);
    }

    public async Task<FlashSaleItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.FlashSaleItems
            .Include(f => f.Product)
            .Include(f => f.FlashSale)
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task AddAsync(FlashSaleItem entity, CancellationToken cancellationToken = default)
    {
        await _context.FlashSaleItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FlashSaleItem entity, CancellationToken cancellationToken = default)
    {
        _context.FlashSaleItems.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var existing = await _context.FlashSaleItems.FindAsync([id], cancellationToken);
        if (existing is null)
            return;

        _context.FlashSaleItems.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
