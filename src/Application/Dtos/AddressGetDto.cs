namespace Application.Dtos;

public class AddressGetDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? Apartment { get; set; }
    public string? PostalCode { get; set; }
}