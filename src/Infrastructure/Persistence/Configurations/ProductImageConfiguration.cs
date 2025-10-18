using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pi => pi.IsMain)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(pi => pi.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(pi => pi.UpdatedAt)
            .IsRequired(false);
        
        builder.Property(pi => pi.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(pi => new { pi.ProductId, pi.IsMain });
        builder.HasIndex(pi => pi.IsDeleted);
    }
}
