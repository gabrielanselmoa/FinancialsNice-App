using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers;

public static class RoleMapper
{
    public static RoleResponse ToResponse(Role role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = role.Permissions?.Select(PermissionMapper.ToResponse).ToList()
        };
    }

    public static Role ToDomain(RoleRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Permissions = request.Permissions?.Select(PermissionMapper.ToDomain).ToList()
        };
    }

    public static Role ToUpdate (Role role, RoleUpdate update)
    {
        if (update.Name != null && update.Name != role.Name)
            role.Name = update.Name;

        return role;
    }
}