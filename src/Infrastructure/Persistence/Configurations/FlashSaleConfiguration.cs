using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FlashSaleConfiguration : IEntityTypeConfiguration<FlashSale>
{
    public void Configure(EntityTypeBuilder<FlashSale> builder)
    {
        builder.ToTable("FlashSales");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(f => f.DiscountPercent)
    .IsRequired()
    .HasColumnType("float");

        builder.HasMany(f => f.Items)
               .WithOne(i => i.FlashSale)
               .HasForeignKey(i => i.FlashSaleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasCheckConstraint("CK_FlashSales_EndAfterStart", "[EndTime] > [StartTime]");
    }
}
