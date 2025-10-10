using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductImageRepository(AppDbContext context) : IProductImageRepository
{
    public async Task<IEnumerable<ProductImage>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.ProductImages.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<ProductImage?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.ProductImages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<ProductImage> AddAsync(ProductImage entity, CancellationToken cancellationToken = default)
    {
        await context.ProductImages.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ProductImage> UpdateAsync(ProductImage entity, CancellationToken cancellationToken = default)
    {
        context.ProductImages.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var image = await context.ProductImages.FindAsync([id], cancellationToken);
        if (image is not null)
        {
            context.ProductImages.Remove(image);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
