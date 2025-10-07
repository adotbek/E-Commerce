namespace Application.Dtos;

public class AddressUpdateDto
{
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? Apartment { get; set; }
    public string? PostalCode { get; set; }
}