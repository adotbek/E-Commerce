using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlists");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
               .WithMany(u => u.Wishlists)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Items)
               .WithOne(i => i.Wishlist)
               .HasForeignKey(i => i.WishlistId);
    }
}
