using Application.Common.Interfaces.Repositories;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BannerRepository(AppDbContext context) : IBannerRepository
{
    public async Task<Banner> CreateAsync(Banner entity)
    {
        await context.Banners.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Banner?> GetByIdAsync(long id)
    {
        return await context.Banners.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<ICollection<Banner>> GetAllAsync()
    {
        return await context.Banners.OrderByDescending(b => b.Id).ToListAsync();
    }

    public async Task<Banner> UpdateAsync(Banner entity)
    {
        context.Banners.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var banner = await context.Banners.FirstOrDefaultAsync(b => b.Id == id);
        if (banner is null)
            return false;

        context.Banners.Remove(banner);
        await context.SaveChangesAsync();
        return true;
    }
}
