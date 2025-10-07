namespace Domain.Entities;

public class Coupon
{
    public long Id { get; set; }
    public string Code { get; set; } = default!;
    public double DiscountPercent { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ValidUntil { get; set; }
}
