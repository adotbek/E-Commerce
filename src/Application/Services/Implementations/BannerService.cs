using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class BannerRepository : IBannerRepository
{
    private readonly AppDbContext _context;

    public BannerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Banner entity)
    {
        await _context.Banners.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Banner?> GetByIdAsync(long id)
    {
        return await _context.Banners
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<ICollection<Banner>> GetAllAsync()
    {
        return await _context.Banners
            .OrderByDescending(b => b.Id)
            .ToListAsync();
    }

    public async Task UpdateAsync(Banner entity)
    {
        _context.Banners.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner is null)
            throw new KeyNotFoundException($"Banner with Id={id} not found.");

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync();
    }
}
