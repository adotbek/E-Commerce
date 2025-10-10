namespace Application.DTOs;

public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int StockQuantity { get; set; }
    public string? Brand { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNewArrival { get; set; }
}
