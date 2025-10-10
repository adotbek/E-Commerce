using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Products.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Product> AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        context.Products.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FindAsync([id], cancellationToken);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
