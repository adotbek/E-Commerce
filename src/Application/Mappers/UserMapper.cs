using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class UserMapper
{
    public static User ToEntity(this UserCreateDto dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName
        };
    }

    public static UserGetDto ToGetDto(this User entity)
    {
        return new UserGetDto
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            UserName = entity.UserName,
            Email = entity.Email,
            ProfileImgUrl = entity.ProfileImgUrl,
            //TelegramId = entity.TelegramId,
            RoleName = entity.Role.Name,
            PaymentOptionId = entity.PaymentOptionId
        };
    }
}
