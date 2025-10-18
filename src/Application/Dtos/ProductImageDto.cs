namespace Application.DTOs;

public class ProductImageDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
    public bool IsMain { get; set; }
    public DateTime CreatedAt { get; set; }
}
