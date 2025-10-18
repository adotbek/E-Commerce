public class PaymentOptionCreateDto
{
    public long UserId { get; set; }
    public string CardHolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public int ExpiryDate { get; set; }
    public int ExpiryMonth { get; set; }   
    public int ExpiryYear { get; set; }
    public string CardType { get; set; } = default!;
}
