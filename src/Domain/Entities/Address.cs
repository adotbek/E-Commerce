namespace Domain.Entities;

public class Address
{
    public long Id { get; set; }
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? Apartment { get; set; }
    public string? PostalCode { get; set; }

    public User User { get; set; } = default!;
    public long UserId { get; set; } 
}
