namespace Application.Dtos;

public class OrderCreateDto
{
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
}