using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class PayerReceiverConfiguration : IEntityTypeConfiguration<PayerReceiver>
{
    public void Configure(EntityTypeBuilder<PayerReceiver> builder)
    {
        // TABLE
        builder.ToTable("PayerReceivers");
        
        // ATTRIBUTES
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(p => p.Description).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(p => p.ImageUrl).HasMaxLength(200).HasColumnType("VARCHAR(200)");
        
        builder.Property(p => p.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();
        
        builder.Property(p => p.Type)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<UserType>())
            .HasDefaultValue(UserType.PERSON).IsRequired();
        
        builder.Property(p => p.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(p => p.ModifiedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        // RELATIONSHIP
        builder.HasOne(p => p.Owner).WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}