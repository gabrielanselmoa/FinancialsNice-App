using FinancialsNice.Application.Dtos.Permissions;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers;

public static class PermissionMapper
{
    public static PermissionResponse ToResponse(Permission permission)
    {
        return new PermissionResponse
        {
            Id = permission.Id,
            Name = permission.Name,
            SlugName = permission.SlugName,
        };
    }

    public static Permission ToDomain(PermissionRequest request)
    {
        return new Permission
        {
            Name = request.Name
        };
    }

    public static Permission ToUpdate (Permission permission, PermissionUpdate update)
    {
        if (update.Name != null && update.Name != permission.Name)
            permission.Name = update.Name;
        if (update.SlugName != null && update.SlugName != permission.SlugName)
            permission.SlugName = update.SlugName;

        return permission;
    }
}