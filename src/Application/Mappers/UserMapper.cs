using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Convertor;

public class UserMapper
{
    public static User ToUserEntity (UserCreateDto dto, string passwordHash, string passwordSalt)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Hash = passwordHash,
            Salt = passwordSalt,                        
                          
        };
    }
}
//public string Hash { get; set; }
//    public string Salt { get; set; }
//    public string? GoogleId { get; set; }
//    public string? ProfileImgUrl { get; set; }

//    public long RoleId { get; set; }
//    public Role Role { get; set; }

//    public long? ConfirmerId { get; set; }
//    public UserConfirmer? Confirmer { get; set; }

//    public ICollection<RefreshToken> RefreshTokens { get; set; }
