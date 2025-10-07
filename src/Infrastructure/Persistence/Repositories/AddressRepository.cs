using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository(AppDbContext context) : IAddressRepository
{
    public async Task<Address> CreateAsync(Address entity)
    {
        await context.Addresses.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Address?> GetByIdAsync(long id)
    {
        return await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ICollection<Address>> GetByUserIdAsync(long userId)
    {
        return await context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address> UpdateAsync(Address entity)
    {
        context.Addresses.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        if (address is null)
            return false;

        context.Addresses.Remove(address);
        await context.SaveChangesAsync();
        return true;
    }
}
