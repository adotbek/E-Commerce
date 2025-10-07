namespace Domain.Entities;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalPrice { get; set; }

    public User User { get; set; } = default!;
    public ICollection<CartItem>? Items { get; set; }
}
