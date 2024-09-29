using Bike360.Application.Features.Customers.Shared;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
public class CustomerDetailsDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public AddressDto Address { get; set; }
}
