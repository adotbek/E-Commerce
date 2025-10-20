using Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IUserService
{
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId, string userRole);

}
