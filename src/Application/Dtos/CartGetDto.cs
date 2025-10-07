namespace Application.Dtos;

public class CartGetDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalPrice { get; set; }
}