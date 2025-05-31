using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialsNice.Infrastructure.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        // TABLE
        builder.ToTable("Addresses");

        // ATTRIBUTES
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Street).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Neighborhood).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.City).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.State).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.ZipCode).HasMaxLength(50).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(p => p.Complement).HasMaxLength(200).HasColumnType("VARCHAR(200)");
        
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(u => u.ModifiedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        
        builder.Property(p => p.Status)
            .HasColumnType("VARCHAR(50)")
            .HasConversion(new EnumToStringConverter<Status>())
            .IsRequired();
        
        // RELATIONSHIP
        builder.HasOne(a => a.Owner).WithOne(u => u.Address)
            .HasForeignKey<User>(u => u.AddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}