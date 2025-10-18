namespace Domain.Entities;

public class PaymentOption
{
    public long Id { get; set; }

    public string CardHolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public int ExpiryDate { get; set; }

    public string CardType { get; set; } = default!;

    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public string? PaymentToken { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
