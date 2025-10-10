using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ReviewRepository(AppDbContext context) : IReviewRepository
{
    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Reviews
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Product)
            .ToListAsync(cancellationToken);

    public async Task<Review?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<Review> AddAsync(Review entity, CancellationToken cancellationToken = default)
    {
        await context.Reviews.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Review> UpdateAsync(Review entity, CancellationToken cancellationToken = default)
    {
        context.Reviews.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Reviews.FindAsync([id], cancellationToken);
        if (entity is not null)
        {
            context.Reviews.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
