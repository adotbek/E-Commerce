using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public long? AddressId { get; set; }
    public Address Address { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
