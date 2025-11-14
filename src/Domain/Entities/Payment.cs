using Domain.Enums;

namespace Domain.Entities;

public class Payment
{
    public long Id { get; set; }

    public DateTime? PaidAt { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long OrderId { get; set; }
    public Order Order { get; set; }

    public long CardId { get; set; }
    public Card Card { get; set; }
}
