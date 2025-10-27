using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Hash { get; set; }
    public string Salt { get; set; }
    public string? GoogleId { get; set; }
    public string? ProfileImgUrl { get; set; }
    public string Email { get; set; }
    public long? TelegramId { get; set; }

    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long? ConfirmerId { get; set; }
    public UserConfirmer? Confirmer { get; set; }

    public long PaymentOptionId { get; set; }
    public PaymentOption PaymentOption { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }

    public ICollection<Address>? Addresses { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Wishlist>? Wishlists { get; set; }
    public ICollection<Cart>? Carts { get; set; }
}
