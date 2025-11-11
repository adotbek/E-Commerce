namespace Domain.Entities;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public double Rating { get; set; }
    public string? ImageUrl { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}
