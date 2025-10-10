using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.Method)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.TransactionId)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasMaxLength(30)
               .HasDefaultValue("Pending");

        //builder.HasOne(x => x.User)
        //       .WithMany()
        //       .HasForeignKey(x => x.UserId)
        //       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Order)
               .WithOne()
               .HasForeignKey<Payment>(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PaymentOption)
               .WithMany()
               .HasForeignKey(x => x.PaymentOptionId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
