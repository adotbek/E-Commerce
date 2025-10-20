using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Core.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteUserByIdAsync(long userId, string userRole)
    {
        if (userRole == "SuperAdmin")
        {
            await _repository.DeleteUserAsync(userId);
        }
        else if (userRole == "Admin")
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user.Role.Name == "User")
            {
                await _repository.DeleteUserAsync(userId);
            }
            else
            {
                throw new NotAllowedException("Admin can not delete Admin or SuperAdmin");
            }
        }
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        await _repository.UpdateUserRoleAsync(userId, userRole);
    }
}
