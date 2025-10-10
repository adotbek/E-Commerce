namespace Application.Dtos;

public class WishlistGetDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public List<WishlistItemGetDto>? Items { get; set; }
}
