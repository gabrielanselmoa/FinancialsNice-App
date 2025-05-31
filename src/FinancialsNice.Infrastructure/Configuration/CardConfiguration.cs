using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        // TABLE
        builder.ToTable("Cards");

        // ATTRIBUTES
        builder.HasKey(c => c.Id);
        // builder.HasIndex(c => c.Id).IsUnique();
        // builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Name).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(c => c.Number).HasMaxLength(50).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(c => c.Company).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(c => c.Flag).HasMaxLength(50).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(c => c.ExpiredAt).HasMaxLength(10).HasColumnType("VARCHAR(10)").IsRequired();
        builder.Property(c => c.Colors).HasMaxLength(255).HasColumnType("VARCHAR(255)").IsRequired();
        
        builder.Property(c => c.CardType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<CardType>())
            .IsRequired();
        
        builder.Property(c => c.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();

        // RELATIONSHIPS
        builder.HasOne(c => c.Owner).WithMany(u => u.Cards)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(c => c.Transactions).WithMany();
    }
}