using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleRepository
{
    Task<ICollection<User>> GetAllUsersByRoleAsync(string role);
    Task<List<Role>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}
