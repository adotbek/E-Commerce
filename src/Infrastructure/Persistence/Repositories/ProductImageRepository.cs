using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository : IProductImageRepository
{
    private readonly AppDbContext _context;

    public ProductImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await _context.ProductImages
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductImage?> GetByIdAsync(long id)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task<long> AddAsync(ProductImage entity)
    {
        await _context.ProductImages.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(ProductImage entity)
    {
        _context.ProductImages.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.ProductImages.FindAsync(id);
        if (existing is null)
            return;

        _context.ProductImages.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(long productId)
    {
        return await _context.ProductImages
            .Where(pi => pi.ProductId == productId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductImage?> GetMainImageByProductIdAsync(long productId)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsMain && !pi.IsDeleted);
    }

    public async Task SetMainImageAsync(long imageId, long productId)
    {
        var images = await _context.ProductImages
            .Where(pi => pi.ProductId == productId)
            .ToListAsync();

        foreach (var img in images)
            img.IsMain = img.Id == imageId;

        _context.ProductImages.UpdateRange(images);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(long id)
    {
        var existing = await _context.ProductImages.FindAsync(id);
        if (existing is null)
            return;

        existing.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
