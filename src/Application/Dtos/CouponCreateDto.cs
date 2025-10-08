namespace Application.Dtos;
public class CouponCreateDto
{
    public string Code { get; set; } = default!;
    public double DiscountPercent { get; set; }
    public DateTime ValidUntil { get; set; }
}