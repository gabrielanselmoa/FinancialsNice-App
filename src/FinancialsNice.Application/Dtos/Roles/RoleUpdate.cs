using System.ComponentModel.DataAnnotations;
using FinancialsNice.Application.Dtos.Permissions;

namespace FinancialsNice.Application.Dtos.Roles
{
    public record RoleUpdate(
        [Required, StringLength(100)] string? Name,
        ICollection<PermissionRequest>? Permissions
    );
}