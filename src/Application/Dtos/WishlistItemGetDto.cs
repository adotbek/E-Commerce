namespace Application.Dtos;

public class WishlistItemGetDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal ProductPrice { get; set; }
}
