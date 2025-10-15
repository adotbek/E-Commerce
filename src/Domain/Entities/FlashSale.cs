namespace Domain.Entities;

public class FlashSale
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<FlashSaleItem> Items { get; set; } = new List<FlashSaleItem>();
}
