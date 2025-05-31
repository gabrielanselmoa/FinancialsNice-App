using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Permissions
{
    public record PermissionUpdate(
        [StringLength(100)] string? Name,
        [StringLength(100)] string? SlugName
    );
}