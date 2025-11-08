namespace Application.Dtos;

public class AddressCreateDto
{
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public long UserId { get; set; }
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? Apartment { get; set; }
    public string? PostalCode { get; set; }
}
