namespace Application.Dtos;

public class BannerCreateDto
{
    public string Title { get; set; } = default!;
    public string? Subtitle { get; set; }
    public double? DiscountPercent { get; set; }
    public string? ImageUrl { get; set; }
    public string? LinkUrl { get; set; }
}