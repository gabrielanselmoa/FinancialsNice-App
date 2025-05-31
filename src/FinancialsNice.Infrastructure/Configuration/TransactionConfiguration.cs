using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        // TABLE
        builder.ToTable("Transactions");

        // ATTRIBUTES
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code).HasMaxLength(50).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(t => t.Description).HasMaxLength(255).HasColumnType("VARCHAR(255)").IsRequired(false);
        builder.Property(t => t.Currency).HasMaxLength(10).HasColumnType("VARCHAR(10)").HasDefaultValue("BRL")
            .IsRequired();
        builder.Property(t => t.Amount).HasColumnType("DECIMAL(10,2)").IsRequired();
        builder.Property(t => t.Email).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.ModifiedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.ScheduledAt)
            .HasColumnType("date")
            .HasDefaultValueSql("CURRENT_DATE").IsRequired();

        builder.Property(t => t.Category)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Category>()).IsRequired();

        builder.Property(t => t.TransactionType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<TransactionType>()).IsRequired();

        builder.Property(t => t.TransactionStatus)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<TransactionStatus>())
            .HasDefaultValue(TransactionStatus.PENDING).IsRequired();

        builder.Property(t => t.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();

        // RELATIONSHIPS
        builder.HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.PayerReceiver)
            .WithMany()
            .HasForeignKey(t => t.PayerReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Payments)
            .WithOne(p => p.Transaction)
            .HasForeignKey(p => p.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Goal)
            .WithMany(g => g.Transactions)
            .HasForeignKey(t => t.GoalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}