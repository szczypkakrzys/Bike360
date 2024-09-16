namespace Bike360.Application.Features.Customers.Commands.CreateCustomer;

public class CreateAddressDto
{
    public string Country { get; set; }
    public string Voivodeship { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
}
