using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers;

public class RoleMapper
{
    public static Role ToRoleEntity(RoleCreateDto dto)
    {
        return new Role
        {
            Name = dto.Name,
            Description = dto.Description
        };

    }
    public RoleGetDto ToRoleEntity(Role role)
    {
        return new RoleGetDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
        };
    }
    public static void UpdateEntity(Role role, RoleUpdateDto dto)
    {
        role.Name = dto.Name;
        role.Description = dto.Description;
    }
}
