using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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
            .HasMaxLength(20);

        builder.Property(p => p.CardType)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.ExpiryMonth)
            .IsRequired();

        builder.Property(p => p.ExpiryYear)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);

        builder.Property(p => p.IsDefault)
            .HasDefaultValue(false);

        builder.Property(p => p.PaymentToken)
            .HasMaxLength(100);

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        //builder.HasOne(p => p.User)
        //    .WithMany(u => u.PaymentOptions)
        //    .HasForeignKey(p => p.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
