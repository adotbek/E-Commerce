namespace Application.Dtos;

public class WishlistCreateDto
{
    public long UserId { get; set; }
    public List<long>? ProductIds { get; set; }
}
