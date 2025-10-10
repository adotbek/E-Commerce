using Application.Dtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<UserGetDto>> GetAllAsync();
    Task<UserGetDto> CreateAsync(UserCreateDto dto);
    Task<bool> DeleteAsync(long id);
}
