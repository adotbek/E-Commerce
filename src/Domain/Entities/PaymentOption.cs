namespace Domain.Entities;

public class PaymentOption
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string CardHolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public string CardType { get; set; } = default!; // VISA, MasterCard

    public User User { get; set; } = default!;
}
