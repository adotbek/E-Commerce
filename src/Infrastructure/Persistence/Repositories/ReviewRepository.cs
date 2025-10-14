using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(long id)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Review> AddAsync(Review entity)
    {
        await _context.Reviews.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Review> UpdateAsync(Review entity)
    {
        _context.Reviews.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.Reviews.FindAsync(id);
        if (entity is null) return false;

        _context.Reviews.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
