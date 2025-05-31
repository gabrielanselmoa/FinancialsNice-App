using System.ComponentModel.DataAnnotations;
using FinancialsNice.Application.Dtos.Permissions;

namespace FinancialsNice.Application.Dtos.Roles
{
    public record RoleRequest(
        [Required, StringLength(100)] string Name,
        ICollection<PermissionRequest>? Permissions
    );
}