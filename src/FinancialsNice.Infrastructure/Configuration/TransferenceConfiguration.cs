using FinancialsNice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialsNice.Infrastructure.Configuration;

public class TransferenceConfiguration : IEntityTypeConfiguration<Transference>
{
    public void Configure(EntityTypeBuilder<Transference> builder)
    {
        // TABLE
        builder.ToTable("Transferences");
        
        // ATTRIBUTES
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Description).HasColumnType("varchar(250)");
        builder.Property(t => t.Currency).HasColumnType("varchar(3)").IsRequired();
        
        builder.Property(t => t.SentAt)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue), 
                v => DateOnly.FromDateTime(v))
            .HasColumnType("date")
            .HasDefaultValueSql("CURRENT_DATE")
            .IsRequired();
        
        // RELATIONSHIPS
        builder.HasOne(t => t.Goal)
            .WithMany(g => g.Transferences)
            .HasForeignKey(t => t.GoalId)
            .OnDelete(DeleteBehavior.Restrict);;
    }
}