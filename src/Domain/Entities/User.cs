using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long? TelegramId { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public string? GoogleId { get; set; }
    public string? ProfileImgUrl { get; set; }

    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long? ConfirmerId { get; set; }
    public UserConfirmer? Confirmer { get; set; }

    public Card? Card { get; set; }

    public ICollection<UserDiscount> Discounts { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<Address> Addresses { get; set; }

}
