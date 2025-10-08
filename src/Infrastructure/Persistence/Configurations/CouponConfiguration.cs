using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.Property(c => c.DiscountPercent)
            .IsRequired()
            .HasPrecision(5, 2); 

        builder.Property(c => c.ValidUntil)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);
    }
}
