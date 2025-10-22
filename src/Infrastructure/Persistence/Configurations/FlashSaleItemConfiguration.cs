using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class FlashSaleItemConfiguration : IEntityTypeConfiguration<FlashSaleItem>
{
    public void Configure(EntityTypeBuilder<FlashSaleItem> builder)
    {
        builder.ToTable("FlashSaleItems");

        builder.HasKey(fsi => fsi.Id);

        builder.HasOne(fsi => fsi.FlashSale)
               .WithMany(fs => fs.Items)
               .HasForeignKey(fsi => fsi.FlashSaleId);


        builder.HasOne(fsi => fsi.Product)
            .WithMany()
            .HasForeignKey(fsi => fsi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fsi => fsi.FlashSale)
            .WithMany()
            .HasForeignKey(fsi => fsi.FlashSaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
