using Application.Dtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserGetDto?> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}
