public class UserGetDto
{
    public long UserId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? ProfileImgUrl { get; set; }
    public long TelegramId { get; set; }
    public long RoleId { get; set; }
    public long PaymentOptionId { get; set; }
}