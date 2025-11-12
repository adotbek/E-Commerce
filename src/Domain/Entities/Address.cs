namespace Domain.Entities;

public class Address
{
    public long Id { get; set; }
    public string Addresss { get; set; } = null!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? Apartment { get; set; }
    public string? PostalCode { get; set; }
    public string FullName { get; set; } = null!;

    public User User { get; set; } = default!;
    public long UserId { get; set; } 
}
