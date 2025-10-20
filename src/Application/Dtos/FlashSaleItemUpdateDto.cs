namespace Application.Dtos;
public class FlashSaleItemUpdateDto
{
    public decimal DiscountedPrice { get; set; }
    public long ProductId { get; set; }
    public long FlashSaleId { get; set; }
}

