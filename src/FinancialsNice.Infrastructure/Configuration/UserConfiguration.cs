using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // TABLE
        builder.ToTable("Users");

        // ATTRIBUTES
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(u => u.BirthDate).HasColumnType("date").IsRequired(false);
        builder.Property(u => u.Email).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(u => u.HashedPassword).HasMaxLength(200).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(u => u.Phone).HasMaxLength(20).HasColumnType("VARCHAR(20)");
        builder.Property(u => u.ImgUrl).HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(u => u.Wizard).HasDefaultValue(false).IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(u => u.ModifiedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        builder.Property(u => u.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .HasDefaultValue(Status.ACTIVE).IsRequired();
        
        builder.Property(u => u.Type)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<UserType>())
            .HasDefaultValue(UserType.PERSON).IsRequired();

        // RELATIONSHIPS
        builder.HasOne(u => u.Address).WithOne(a => a.Owner)
            .HasForeignKey<Address>(a =>  a.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Cards).WithOne(c => c.Owner)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users);
        builder.HasOne(u => u.Wallet).WithOne(w => w.Owner)
            .HasForeignKey<Wallet>(w => w.OwnerId);
    }
}