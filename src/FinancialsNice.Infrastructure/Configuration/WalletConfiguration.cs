using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class WalletConfiguration :  IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        // TABLE
        builder.ToTable("Wallets");
        
        // ATTRIBUTES
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name).HasMaxLength(100).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(w => w.Balance)
            .HasColumnType("DECIMAL(10,2)").IsRequired();
        builder.Property(w => w.CreatedAt)
            .HasColumnType("timestamp with time zone").HasDefaultValueSql
            ("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(w => w.ModifiedAt)
            .HasColumnType("timestamp with time zone").HasDefaultValueSql
            ("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(w => w.Status).HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();
        
        // RELATIONSHIPS
        builder.HasOne(w => w.Owner).WithOne(u => u.Wallet)
            .HasForeignKey<User>(u => u.WalletId);
        
        builder.HasMany(w => w.Transactions)
            .WithOne(t => t.Wallet)
            .HasForeignKey(t => t.WalletId);

    }
}