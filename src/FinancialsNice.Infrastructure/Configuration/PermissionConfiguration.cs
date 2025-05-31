using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // TABLE
        builder.ToTable("Permissions");

        // ATTRIBUTES
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.SlugName).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();

        // RELATIONSHIPS
        builder.HasMany(p => p.Roles).WithMany(r => r.Permissions);
    }
}