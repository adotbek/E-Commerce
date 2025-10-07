namespace Domain.Entities;

public class Wishlist
{
    public long Id { get; set; }
    public long UserId { get; set; }

    public User User { get; set; } = default!;
    public ICollection<WishlistItem>? Items { get; set; }
}
