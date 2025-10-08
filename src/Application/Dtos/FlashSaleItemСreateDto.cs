namespace Application.DTOs.FlashSaleItems;

public class FlashSaleItemCreateDto
{
    public long ProductId { get; set; }
    public long FlashSaleId { get; set; }
    public decimal DiscountedPrice { get; set; }
}

