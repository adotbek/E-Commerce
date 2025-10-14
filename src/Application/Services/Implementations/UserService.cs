using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserGetDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return users.Select(UserMapper.ToGetDto).ToList();
    }

    public async Task<UserGetDto?> GetByIdAsync(long id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user is null ? null : UserMapper.ToGetDto(user);
    }

    public async Task<UserGetDto> CreateAsync(UserCreateDto dto)
    {
        var entity = UserMapper.ToEntity(dto);
        await _repository.AddUserAsync(entity);
        return UserMapper.ToGetDto(entity);
    }
   
    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return false;

        await _repository.DeleteUserAsync(user);
        return true;
    }
}
