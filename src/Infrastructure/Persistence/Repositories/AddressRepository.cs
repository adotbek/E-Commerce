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

    public async Task SetDefaultAddressAsync(long userId, long addressId)
    {
        var userAddresses = await _context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();

        if (!userAddresses.Any())
            throw new InvalidOperationException($"User with ID {userId} has no addresses.");

        var currentDefault = await _context.Addresses
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (currentDefault is not null)
        {
            currentDefault.Id = addressId;
            _context.Addresses.Update(currentDefault);
        }
        else
        {
            _context.Addresses.Add(new Address
            {
                UserId = userId,
                Id = addressId
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Address?> GetDefaultAddressAsync(long userId)
    {
        var defaultRecord = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (defaultRecord is null)
            return null;

        return await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == defaultRecord.Id);
    }

    public async Task<bool> ExistsAsync(long id, long userId)
    {
        return await _context.Addresses
            .AnyAsync(a => a.Id == id && a.UserId == userId);
    }
}
