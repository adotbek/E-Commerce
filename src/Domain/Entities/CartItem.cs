namespace Domain.Entities;

public class CartItem
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Cart Cart { get; set; } = default!;
    public Product Product { get; set; } = default!;
    public ProductVariant ProductVariant { get; set; } = null!;

}
