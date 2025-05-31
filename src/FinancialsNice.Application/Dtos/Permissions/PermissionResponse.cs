namespace FinancialsNice.Application.Dtos.Permissions
{
    public record PermissionResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string SlugName { get; init; } = null!;
    }
}