using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialsNice.Infrastructure.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        // TABLE
        builder.ToTable("Tickets");

        // PRIMARY KEY
        builder.HasKey(t => t.Id);

        // PROPERTIES
        builder.Property(t => t.Code)
            .HasMaxLength(15).HasColumnType("VARCHAR(15)").IsRequired();

        builder.Property(t => t.Name)
            .HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();

        builder.Property(t => t.Subject)
            .HasMaxLength(200).HasColumnType("VARCHAR(100)").IsRequired();

        builder.Property(t => t.Message)
            .HasMaxLength(2000).HasColumnType("VARCHAR(2000)").IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();
        
        builder.Property(t => t.ModifiedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(t => t.IsResolved)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(t => t.ResolvedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(t => t.Type)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<TicketType>()
            .IsRequired();

        // RELATIONSHIPS
        builder.HasOne(t => t.Client)
            .WithMany()
            .HasForeignKey(t => t.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ResolvedBy)
            .WithMany()
            .HasForeignKey(t => t.ResolvedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}