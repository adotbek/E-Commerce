using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long AddressId { get; set; }
    public Address Address { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long CartId { get; set; }
    public Cart Cart { get; set; }
}
