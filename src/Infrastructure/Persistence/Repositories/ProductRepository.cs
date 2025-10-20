using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing is null)
            return;

        _context.Products.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(long categoryId)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetFeaturedAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsFeatured)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetNewArrivalsAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsNewArrival)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<Product>();

        return await _context.Products
            .AsNoTracking()
            .Where(p =>
                p.Name.ToLower().Contains(keyword.ToLower()) ||
                (p.Description != null && p.Description.ToLower().Contains(keyword.ToLower())) ||
                (p.Brand != null && p.Brand.ToLower().Contains(keyword.ToLower())))
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task UpdateStockAsync(long id, int quantity)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return;

        product.StockQuantity = quantity;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetOutOfStockAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.StockQuantity <= 0)
            .ToListAsync();
    }

    public async Task<decimal?> GetDiscountPriceAsync(long productId)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId);

        return product?.DiscountPrice;
    }
}
