namespace Domain.Entities;

public class WishlistItem
{
    public long Id { get; set; }
    public long WishlistId { get; set; }
    public long ProductId { get; set; }

    public Wishlist Wishlist { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
