using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SlugName { get; set; } = null!;
    public Status Status { get; set; }
    public ICollection<Role>? Roles { get; set; } = new List<Role>();
}