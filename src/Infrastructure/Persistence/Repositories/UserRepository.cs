using Core.Errors;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<long> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.UserId;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteUserAsync(User user)
    { 
            _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }


    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).Include(_ => _.Role).FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new EntityNotFoundException($"Entity with {userName} not found");
        }
        return user;
    }
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).Include(x => x.Role).FirstOrDefaultAsync(x => x.Confirmer!.Email == email);
        return user;
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        var user = await GetUserByIdAsync(userId);
        var role = await _context.UserRoles.FirstOrDefaultAsync(x => x.Name == userRole);
        if (role == null)
        {
            throw new EntityNotFoundException($"Role : {userRole} not found");
        }
        user.RoleId = role.Id;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByIdAsync(long id)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).Include(_ => _.Role).FirstOrDefaultAsync(x => x.UserId == id);
        if (user == null)
        {
            throw new EntityNotFoundException($"Entity with {id} not found");
        }
        return user;
    }

    public async Task<User> GetUserByGoogleId(string googleId)
    {
        return await _context.Users.Include(x => x.Confirmer).FirstOrDefaultAsync(x => x.GoogleId == googleId);
    }
}
