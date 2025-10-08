namespace Application.Dtos;

public class FlashSaleGetDto
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}