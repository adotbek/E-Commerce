namespace Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string? GoogleId { get; set; }
    public string? ProfileImgUrl { get; set; }

    public long RoleId { get; set; }
    public UserRole Role { get; set; }

    public long? ConfirmerId { get; set; }
    public UserConfirmer? Confirmer { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
