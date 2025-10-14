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
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetByIdAsync(long id)
    {
        return await _context.ProductImages.FindAsync(id);
    }

    public async Task<ProductImage> AddAsync(ProductImage entity)
    {
        await _context.ProductImages.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ProductImage> UpdateAsync(ProductImage entity)
    {
        _context.ProductImages.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.ProductImages.FindAsync(id);
        if (entity is null) return false;

        _context.ProductImages.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
