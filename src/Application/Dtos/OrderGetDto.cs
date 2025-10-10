namespace Application.Dtos;

public class OrderGetDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderItemGetDto>? Items { get; set; }
}