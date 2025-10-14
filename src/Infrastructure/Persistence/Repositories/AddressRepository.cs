using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Address entity)
    {
        await _context.Addresses.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Address?> GetByIdAsync(long id)
    {
        return await _context.Addresses
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ICollection<Address>> GetByUserIdAsync(long userId)
    {
        return await _context.Addresses
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.Id)
            .ToListAsync();
    }

    public async Task UpdateAsync(Address entity)
    {
        _context.Addresses.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _context.Addresses.FindAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Address with Id={id} not found.");

        _context.Addresses.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
