namespace Application.Dtos;

public class OrderItemUpdateDto
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
