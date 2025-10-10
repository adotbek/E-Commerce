using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(o => o.ShippingAddress)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(o => o.PaymentMethod)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(o => o.Status)
               .HasMaxLength(30)
               .HasDefaultValue("Pending");

        builder.Property(o => o.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
