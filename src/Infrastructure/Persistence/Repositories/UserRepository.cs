using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(long id)
        => await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.AsNoTracking().ToListAsync();

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task UpdateAsync(User user)
        => _context.Users.Update(user);

    public async Task DeleteAsync(User user)
        => _context.Users.Remove(user);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
