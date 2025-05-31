using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // TABLE
        builder.ToTable("Roles");

        // ATTRIBUTES
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.Name).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(r => r.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();

        // RELATIONSHIPS
        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles);
        // builder.HasMany(r => r.Users).WithMany(u => u.Roles);
    }
}