using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PaymentOptionConfiguration : IEntityTypeConfiguration<PaymentOption>
{
    public void Configure(EntityTypeBuilder<PaymentOption> builder)
    {
        builder.ToTable("PaymentOptions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CardHolderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CardNumber)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(p => p.ExpiryDate)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(p => p.CardType)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(p => p.User)
            .WithOne(u => u.PaymentOption)
            .HasForeignKey<User>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
