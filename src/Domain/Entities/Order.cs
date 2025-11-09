using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long AddressId { get; set; }

    public User User { get; set; } = default!;
    public ICollection<OrderItem>? Items { get; set; }
}
