using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Permissions
{
    public record PermissionRequest(
        [Required, StringLength(100)] string Name
    );
}