namespace Domain.Entities;

public class CartItem
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public long CartId { get; set; }
    public Cart Cart { get; set; } = default!;

    public long ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;
}
