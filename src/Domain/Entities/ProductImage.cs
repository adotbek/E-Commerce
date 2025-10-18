using Domain.Entities;

public class ProductImage
{
    public long Id { get; set; }

    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public bool IsMain { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
