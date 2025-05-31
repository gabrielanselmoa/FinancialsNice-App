using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // TABLE
        builder.ToTable("Payments");

        // ATTRIBUTES
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).HasColumnType("DECIMAL(10,2)").IsRequired();
        builder.Property(p => p.Installments).IsRequired();
        builder.Property(p => p.ValuePerInstallment).HasColumnType("DECIMAL(10,2)").IsRequired();
        
        builder.Property(p => p.PaymentType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<PaymentType>()).IsRequired();
        
        builder.Property(p => p.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(p => p.ModifiedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        builder.Property(p => p.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();

        // RELATIONSHIPS
        builder.HasOne(p => p.Owner).WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Card).WithMany()
            .HasForeignKey(p => p.CardId).IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}