using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<IEnumerable<User>> GetAllAsync();
    Task <long> AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<User> GetUserByUserNameAsync(string userName);
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task<User> GetUserByIdAsync(long id);
    Task<User> GetUserByGoogleId(string googleId);
    Task<User> GetUserByEmailAsync(string email);

}
