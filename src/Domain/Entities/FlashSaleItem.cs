namespace Domain.Entities;

public class FlashSaleItem
{
    public long Id { get; set; }
    public Product Product { get; set; }
    public long ProductId { get; set; }
    public FlashSale FlashSale { get; set; }
    public long FlashSaleId { get; set; }

    public decimal DiscountedPrice { get; set; }

}
