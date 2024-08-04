namespace Bike360.Application.Features.Shared;

public class AddressDto
{
    //don't need to keep it in shared - can be moved I think do separate
    public int Id { get; set; }
    public string Country { get; set; }
    public string Voivodeship { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; }
}
