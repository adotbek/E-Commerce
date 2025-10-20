namespace Domain.Entities;

public class FlashSaleItem
{
    public long Id { get; set; }
    public Product Product { get; set; } = default!;
    public long ProductId { get; set; }

    public FlashSale FlashSale { get; set; } = default!;
    public long FlashSaleId { get; set; }

}
