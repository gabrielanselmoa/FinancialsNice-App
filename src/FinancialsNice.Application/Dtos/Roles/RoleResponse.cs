using FinancialsNice.Application.Dtos.Permissions;

namespace FinancialsNice.Application.Dtos.Roles
{
    public record RoleResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public ICollection<PermissionResponse>? Permissions { get; init; } = new List<PermissionResponse>();
    }
}