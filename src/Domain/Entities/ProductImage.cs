namespace Domain.Entities;

public class ProductImage
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;

    public Product Product { get; set; } = default!;
}
