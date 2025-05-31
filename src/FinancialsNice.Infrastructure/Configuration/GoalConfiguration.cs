using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        // TABLE
        builder.ToTable("Goals");
        
        // ATTRIBUTES
        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Name).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(g => g.Balance).HasColumnType("DECIMAL(10,2)").IsRequired();
        builder.Property(g => g.Target).HasColumnType("DECIMAL(10,2)").IsRequired();
        builder.Property(g => g.Due).HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        builder.Property(g => g.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(g => g.ModifiedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        builder.Property(g => g.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();
        builder.Property(g => g.GoalType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<GoalType>())
            .IsRequired();
        
        // RELATIONSHIPS
        builder.HasOne(g => g.Owner).WithMany()
            .HasForeignKey(g => g.OwnerId);
        
        builder.HasMany(g => g.Transactions)
            .WithOne(t => t.Goal)
            .HasForeignKey(t => t.GoalId);
    }
}