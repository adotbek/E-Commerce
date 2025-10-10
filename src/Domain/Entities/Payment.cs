namespace Domain.Entities;

public class Payment
{
    public long Id { get; set; }

    public long OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public long? PaymentOptionId { get; set; }
    public PaymentOption? PaymentOption { get; set; }

    public decimal Amount { get; set; }
    public string Method { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
